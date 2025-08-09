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
using System.Reflection;

using Dapper;

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
    public async Task AddAsync(TAggregateRoot entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        string tableName = GetTableName();
        IEnumerable<PropertyInfo> properties = GetEntityProperties(excludeId: true);

        string columns = string.Join(", ", properties.Select(property => property.Name));
        string parameters = string.Join(", ", properties.Select(property => "@" + property.Name));

        string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
        CommandDefinition commandDefinition = new(sql, entity, cancellationToken: cancellationToken);

        await Connection.ExecuteAsync(commandDefinition);
    }

    /// <summary>
    /// Asynchronously deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    public async Task DeleteAsync(TAggregateRoot entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        string tableName = GetTableName();
        string sql = $"DELETE FROM {tableName} WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id);

        CommandDefinition commandDefinition = new(sql, parameters, cancellationToken: cancellationToken);
        await Connection.ExecuteAsync(commandDefinition);
    }

    /// <summary>
    /// Asynchronously updates an entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous update operation.
    /// </returns>
    public async Task UpdateAsync(TAggregateRoot entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        string tableName = GetTableName();
        IEnumerable<PropertyInfo> properties = GetEntityProperties(excludeId: true);

        string setClause = string.Join(", ", properties.Select(property => $"{property.Name} = @{property.Name}"));
        string sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

        CommandDefinition commandDefinition = new(sql, entity, cancellationToken: cancellationToken);
        await Connection.ExecuteAsync(commandDefinition);
    }

    /// <summary>
    /// Gets the list of insertable properties for the aggregate root.
    /// Excludes read-only properties, navigation properties, and optionally the Id.
    /// </summary>
    /// <param name="excludeId">If true, the Id property will be excluded from the result.</param>
    /// <returns>A list of <see cref="PropertyInfo"/> representing the insertable columns.</returns>
    private static List<PropertyInfo> GetEntityProperties(bool excludeId = true)
        => [.. typeof(TAggregateRoot)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead && p.CanWrite)
            .Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string))
            .Where(p => !excludeId || !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))];

    /// <summary>
    /// Gets the table name (basic convention :)
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> instance that contains the table name.
    /// </returns>
    private static string GetTableName() => typeof(TAggregateRoot).Name;
}
