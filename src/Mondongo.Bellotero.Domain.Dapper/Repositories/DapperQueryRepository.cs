// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DapperQueryRepository.cs" company="OscarNET-SOFTware">
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
using System.Linq.Expressions;

using CSharpFunctionalExtensions;

using Dapper;

using Mondongo.Bellotero.Domain.Model;
using Mondongo.Bellotero.Domain.Specifications;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Provides Dapper's repository implementation for query operations (read-only) on an aggregate root entity.
/// </summary>
/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
public class DapperQueryRepository<TAggregateRoot> : IQueryRepository<TAggregateRoot>
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
    /// Initializes a new instance of the <see cref="DapperQueryRepository{TAggregateRoot}" /> class.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <param name="transaction">The transaction.</param>
    public DapperQueryRepository(DbConnection connection, DbTransaction? transaction)
    {
        ArgumentNullException.ThrowIfNull(connection, nameof(connection));
        Connection = connection;
        Transaction = transaction;
    }

    /// <summary>
    /// Asynchronously checks whether one or more entities match a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous check operation.
    /// Its result contains a <c>true</c> value if one or more entities meet the specified criteria.
    /// </returns>
    public async Task<bool> AnyAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT 1 FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);
        int? result = await Connection.QueryFirstOrDefaultAsync<int?>(commandDefinition);

        return result.HasValue;
    }

    /// <summary>
    /// Asynchronously counts the entities.
    /// </summary>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous counting operation.
    /// Its result contains the total number of entities.
    /// </returns>
    public Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        string sql = $"SELECT COUNT(*) FROM {GetTableName()}";
        CommandDefinition commandDefinition = new(sql, cancellationToken: cancellationToken);

        return Connection.ExecuteScalarAsync<long>(commandDefinition);
    }

    /// <summary>
    /// Asynchronously counts the entities that match a given specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous counting operation.
    /// Its result contains the total number of entities that meet the specified specification.
    /// </returns>
    public Task<long> CountAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT COUNT(*) FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);

        return Connection.ExecuteScalarAsync<long>(commandDefinition);
    }

    /// <summary>
    /// Asynchronously checks if exists a given entity.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous check operation.
    /// Its result contains a <c>true</c> value if the entity exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> ExistsAsync(object entityId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entityId, nameof(entityId));

        var specification = new ByIdSpecification<TAggregateRoot>(entityId);
        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT 1 FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);
        int? result = await Connection.QueryFirstOrDefaultAsync<int?>(commandDefinition);

        return result.HasValue;
    }

    /// <summary>
    /// Asynchronously retrieves the first entity that matches a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the first entity found, if it meets the specified criteria.
    /// </returns>
    public async Task<Maybe<TAggregateRoot>> FirstAsync(ISpecification<TAggregateRoot> specification,
                                                        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT * FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);

        return await Connection.QueryFirstOrDefaultAsync<TAggregateRoot>(commandDefinition);
    }

    /// <summary>
    /// Asynchronously retrieves all entities that match a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains a sequence of entities that meet the specified criteria.
    /// </returns>
    public async Task<IEnumerable<TAggregateRoot>> GetAllAsync(ISpecification<TAggregateRoot> specification,
                                                               CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT * FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);
        return await Connection.QueryAsync<TAggregateRoot>(commandDefinition);
    }

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the found entity, if one exists.
    /// </returns>
    public async Task<Maybe<TAggregateRoot>> GetByIdAsync(object entityId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entityId, nameof(entityId));

        var specification = new ByIdSpecification<TAggregateRoot>(entityId);
        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT * FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);
        return await Connection.QueryFirstOrDefaultAsync<TAggregateRoot>(commandDefinition);
    }

    /// <summary>
    /// Finds the entities that match a given criteria.
    /// </summary>
    /// <param name="queryConstraint">The query constraint.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A paged collection containing the entities that match the specified criteria.
    /// </returns>
    public async Task<PagedCollection<TAggregateRoot>> GetPagedAsync(QueryConstraint<TAggregateRoot> queryConstraint,
                                                                     CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryConstraint, nameof(queryConstraint));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(queryConstraint.Specification);

        string sortColumn = GetOrderByColumns(queryConstraint.SortExpression);
        string sortDirection = queryConstraint.SortAscending ? "ASC" : "DESC";
        string tableName = GetTableName();

        string countSql = $"SELECT COUNT(*) FROM {tableName} WHERE {whereSql}";
        CommandDefinition countCmd = new(countSql, sqlParams, cancellationToken: cancellationToken);
        long totalCount = await Connection.ExecuteScalarAsync<long>(countCmd);

        int offset = (queryConstraint.PageIndex - 1) * queryConstraint.PageSize;
        int fetch = queryConstraint.PageSize;

        string pageSql =
            $"SELECT * FROM {tableName} " +
            $"WHERE {whereSql} " +
            $"ORDER BY {sortColumn} {sortDirection} " +
            $"OFFSET {offset} ROWS FETCH NEXT {fetch} ROWS ONLY";

        CommandDefinition pageCmd = new(pageSql, sqlParams, cancellationToken: cancellationToken);
        IEnumerable<TAggregateRoot> entities = await Connection.QueryAsync<TAggregateRoot>(pageCmd);

        return new PagedCollection<TAggregateRoot>(entities, totalCount, queryConstraint.PageIndex, queryConstraint.PageSize);
    }

    /// <summary>
    /// Asynchronously retrieves the unique entity that matches a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the unique entity found, if it meets the specified criteria.
    /// </returns>
    public async Task<Maybe<TAggregateRoot>> SingleAsync(ISpecification<TAggregateRoot> specification,
                                                         CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var visitor = new DapperSpecificationVisitor<TAggregateRoot>();
        (string whereSql, DynamicParameters sqlParams) = visitor.Visit(specification);

        string sql = $"SELECT * FROM {GetTableName()} WHERE {whereSql}";
        CommandDefinition commandDefinition = new(sql, sqlParams, cancellationToken: cancellationToken);
        var results = (await Connection.QueryAsync<TAggregateRoot>(commandDefinition)).ToList();

        if (results.Count > 1)
        {
            throw new InvalidOperationException(
                $"More than one {typeof(TAggregateRoot).Name} matches the specified criteria.");
        }

        return results.Count == 0
            ? Maybe<TAggregateRoot>.None
            : Maybe<TAggregateRoot>.From(results[0]);
    }

    /// <summary>
    /// Extracts the column names from a sorting expression for use in SQL ORDER BY.
    /// Supports both single property expressions (e.g., x => x.Id)
    /// and anonymous object expressions with multiple properties (e.g., x => new { x.Id, x.Name }).
    /// </summary>
    /// <param name="sortExpression">The lambda expression representing the column or columns to use for sorting.</param>
    /// <returns>
    /// A <see cref="string"/> containing the list of column names, separated by commas, suitable for SQL ORDER BY.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Thrown when the sort expression is not a member access or an anonymous new-expression.
    /// </exception>
    private static string GetOrderByColumns(Expression<Func<TAggregateRoot, object>> sortExpression)
    {
        if (sortExpression.Body is MemberExpression memberExpr)
        {
            // Simple case: x => x.Property
            return memberExpr.Member.Name;
        }

        if (sortExpression.Body is NewExpression newExpr)
        {
            // Multiple case: x => new { x.Property1, x.Property2, ... }
            IEnumerable<string> columns = newExpr.Members!.Select(m => m.Name);
            return string.Join(", ", columns);
        }

        throw new NotSupportedException("SortExpression must be a member access or anonymous new expression.");
    }

    /// <summary>
    /// Gets the table name (basic convention :)
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> instance that contains the table name.
    /// </returns>
    private static string GetTableName() => typeof(TAggregateRoot).Name;
}
