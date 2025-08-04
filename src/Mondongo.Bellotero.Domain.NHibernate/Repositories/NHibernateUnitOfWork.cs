// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateUnitOfWork.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Model;

using NHibernate;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Provides NHibernate's Unit of Work implementation that coordinates the work of multiple repositories
/// by managing atomic transactions across them.
/// </summary>
/// <remarks>
/// The Unit of Work pattern ensures that changes across repositories are committed as a single transaction,
/// maintaining consistency and simplifying transaction management.
/// </remarks>
public sealed class NHibernateUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Stores the database session.
    /// </summary>
    private readonly ISession _session;

    /// <summary>
    /// Stores the database transaction.
    /// </summary>
    private ITransaction? _transaction;

    /// <summary>
    /// Stores the value indicating whether this instante is disposed.
    /// </summary>
    private volatile bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="NHibernateUnitOfWork" /> class.
    /// </summary>
    /// <param name="session">The database session.</param>
    public NHibernateUnitOfWork(ISession session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));

        _session = session;
        _transaction = _session.BeginTransaction();
    }

    /// <summary>
    /// Gets a query repository for read-only operations on the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <returns>An <see cref="IQueryRepository{TAggregateRoot}"/> for querying entities.</returns>
    public IQueryRepository<TAggregateRoot> QueryRepository<TAggregateRoot>()
        where TAggregateRoot : IAggregateRoot => throw new NotImplementedException();

    /// <summary>
    /// Gets a command repository for write operations (create, update, delete) on the specified entity type.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity type.</typeparam>
    /// <returns>An <see cref="ICommandRepository{TAggregateRoot}"/> for modifying entities.</returns>
    public ICommandRepository<TAggregateRoot> CommandRepository<TAggregateRoot>()
        where TAggregateRoot : IAggregateRoot => throw new NotImplementedException();

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
        _transaction.Dispose();
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
        _transaction.Dispose();
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

                _session.Dispose();
            }

            _isDisposed = true;
        }
    }
}
