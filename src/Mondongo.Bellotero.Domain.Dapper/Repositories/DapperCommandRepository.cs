// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DapperCommandRepository.cs" company="OscarNET-SOFTware">
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

using System.Data.Common;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Provides Dapper's repository implementation for command operations (write) on an aggregate root entity.
/// </summary>
/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
public class DapperCommandRepository<TAggregateRoot> : ICommandRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    /// <summary>
    /// Stores the connection.
    /// </summary>
    protected readonly DbConnection Connection;

    /// <summary>
    /// Stores the transaction.
    /// </summary>
    protected readonly DbTransaction? Transaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="DapperCommandRepository{TAggregateRoot}" /> class.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <param name="transaction">The transaction.</param>
    public DapperCommandRepository(DbConnection connection, DbTransaction? transaction)
    {
        ArgumentNullException.ThrowIfNull(connection, nameof(connection));
        Connection = connection;
        Transaction = transaction;
    }

    /// <summary>
    /// Asynchronously adds an entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous add operation.
    /// </returns>
    public Task AddAsync(TAggregateRoot entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    /// <summary>
    /// Asynchronously deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    public Task DeleteAsync(TAggregateRoot entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    /// <summary>
    /// Asynchronously updates an entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous update operation.
    /// </returns>
    public Task UpdateAsync(TAggregateRoot entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
