// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityFrameworkUnitOfWork.cs" company="OscarNET-SOFTware">
// ···
//      Mondongo.Dehesa.Framework - Just a set of essential libraries for DotNET: clean, simple and ready to use.
// ···
//      Copyright (c) 2025 Oscar Fernandez Gonzalez a.k.a. Osc@rNET
//      Licensed under the MIT License. See the 'LICENSE.md' file for details.
// ···
//      Third-party components are used in this project. For full license texts,
//      see the 'licenses' folder and the 'THIRD-PARTY-NOTICES.md' file.
// ···
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Provides Entity Framework's Unit of Work implementation that coordinates the work of multiple repositories
/// by managing atomic transactions across them.
/// </summary>
/// <remarks>
/// The Unit of Work pattern ensures that changes across repositories are committed as a single transaction,
/// maintaining consistency and simplifying transaction management.
/// </remarks>
public sealed class EntityFrameworkUnitOfWork : IEntityFrameworkUnitOfWork
{
    /// <summary>
    /// Stores the database context.
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// Stores the factories of the custom query repositories.
    /// </summary>
    private readonly Dictionary<Type, Func<DbContext, object>> _queryRepoFactories = [];

    /// <summary>
    /// Stores the factories of the custom command repositories.
    /// </summary>
    private readonly Dictionary<Type, Func<DbContext, object>> _commandRepoFactories = [];

    /// <summary>
    /// Stores the database transaction.
    /// </summary>
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Stores the value indicating whether this instante is disposed.
    /// </summary>
    private volatile bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityFrameworkUnitOfWork" /> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public EntityFrameworkUnitOfWork(DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        _context = context;
    }

    /// <summary>
    /// Gets a query repository for read-only operations on the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <returns>An <see cref="IQueryRepository{TAggregateRoot}"/> for querying entities.</returns>
    public IQueryRepository<TAggregateRoot> QueryRepository<TAggregateRoot>()
        where TAggregateRoot : class, IAggregateRoot
        => (IQueryRepository<TAggregateRoot>)GetOrCreateQueryRepository<TAggregateRoot>();

    /// <summary>
    /// Gets a command repository for write operations (create, update, delete) on the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <returns>An <see cref="ICommandRepository{TAggregateRoot}"/> for modifying entities.</returns>
    public ICommandRepository<TAggregateRoot> CommandRepository<TAggregateRoot>()
        where TAggregateRoot : class, IAggregateRoot
        => (ICommandRepository<TAggregateRoot>)GetOrCreateCommandRepository<TAggregateRoot>();

    /// <summary>
    /// Registers a custom query repository implementation for the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <typeparam name="TRepository">Repository implementation type.</typeparam>
    /// <param name="factory">Factory function that receives the context
    /// and produces a query repository instance.</param>
    public void RegisterQueryRepository<TAggregateRoot, TRepository>(Func<DbContext, TRepository> factory)
        where TRepository : class, IQueryRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
        => _queryRepoFactories[typeof(TAggregateRoot)] = (context) => factory(context);

    /// <summary>
    /// Registers a custom command repository implementation for the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <typeparam name="TRepository">Repository implementation type.</typeparam>
    /// <param name="factory">Factory function that receives the context
    /// and produces a command repository instance.</param>
    public void RegisterCommandRepository<TAggregateRoot, TRepository>(Func<DbContext, TRepository> factory)
        where TRepository : class, ICommandRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
        => _commandRepoFactories[typeof(TAggregateRoot)] = (context) => factory(context);

    /// <summary>
    /// Asynchronously opens the database connection (if not already open) and
    /// begins a new transaction for the current unit of work.
    /// </summary>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.
    /// The default value is <see cref="CancellationToken.None" />.</param>
    /// <returns>
    /// A task that represents the asynchronous operation of starting the transaction.
    /// </returns>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commits all changes made within the current unit of work as a single transaction.
    /// </summary>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.
    /// The default value is <see cref="CancellationToken.None" />.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("Transaction has not been started.");
        }

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Rolls back all changes tracked by the current unit of work.
    /// </summary>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.
    /// The default value is <see cref="CancellationToken.None" />.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("Transaction has not been started.");
        }

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
    /// <c>false</c> to release only unmanaged resources.</param>
    private void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                _context.Dispose();
            }

            _isDisposed = true;
        }
    }

    /// <summary>
    /// Retrieves or creates the query repository instance for the specified entity type.
    /// If a custom query repository factory has been registered, it will be used;
    /// otherwise, a generic query repository is created.
    /// </summary>
    private object GetOrCreateQueryRepository<TAggregateRoot>()
        where TAggregateRoot : class, IAggregateRoot
    {
        Type aggregateRootType = typeof(TAggregateRoot);
        if (_queryRepoFactories.TryGetValue(aggregateRootType, out Func<DbContext, object>? factory))
        {
            return factory(_context);
        }

        return Activator.CreateInstance(
            type: typeof(EntityFrameworkQueryRepository<>).MakeGenericType(aggregateRootType),
            args: [_context]
        )!;
    }

    /// <summary>
    /// Retrieves or creates the command repository instance for the specified entity type.
    /// If a custom command repository factory has been registered, it will be used;
    /// otherwise, a generic command repository is created.
    /// </summary>
    private object GetOrCreateCommandRepository<TAggregateRoot>()
        where TAggregateRoot : class, IAggregateRoot
    {
        Type aggregateRootType = typeof(TAggregateRoot);
        if (_commandRepoFactories.TryGetValue(aggregateRootType, out Func<DbContext, object>? factory))
        {
            return factory(_context);
        }

        return Activator.CreateInstance(
            type: typeof(EntityFrameworkCommandRepository<>).MakeGenericType(aggregateRootType),
            args: [_context]
        )!;
    }
}
