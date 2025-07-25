// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryConstraint.cs" company="OscarNET-SOFTware">
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

using System.Linq.Expressions;

using Mondongo.Bellotero.Domain.Model;
using Mondongo.Bellotero.Domain.Specifications;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Represents a query constraint.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="QueryConstraint{TEntity}" /> class.
/// </remarks>
/// <param name="specification">The specification.</param>
public sealed class QueryConstraint<TEntity>(ISpecification<TEntity> specification)
    where TEntity : IEntity
{
    /// <summary>
    /// Defines the default page index.
    /// </summary>
    internal const int DefaultPageIndex = 1;

    /// <summary>
    /// Defines the default page size.
    /// </summary>
    internal const int DefaultPageSize = 100;

    /// <summary>
    /// Gets or sets the page index.
    /// </summary>
    /// <value>
    /// The page index.
    /// </value>
    public int PageIndex { get; private set; } = DefaultPageIndex;

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    /// <value>
    /// The page size.
    /// </value>
    public int PageSize { get; private set; } = DefaultPageSize;

    /// <summary>
    /// Gets a value indicating whether the sort is ascending.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the sort is ascending; otherwise, <c>false</c>.
    /// </value>
    public bool SortAscending { get; private set; } = true;

    /// <summary>
    /// Gets the sort expression.
    /// </summary>
    /// <value>
    /// The sort expression.
    /// </value>
    public Expression<Func<TEntity, object>> SortExpression { get; private set; } = entity => entity.Id;

    /// <summary>
    /// Gets the specification.
    /// </summary>
    /// <value>
    /// The specification.
    /// </value>
    public ISpecification<TEntity> Specification { get; }
        = specification ?? throw new ArgumentNullException(nameof(specification));

    /// <summary>
    /// Sets the sort order.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="ascending">It set to <c>true</c> for ascending sort order.</param>
    /// <returns>
    /// This instance with the updated sort order.
    /// </returns>
    public QueryConstraint<TEntity> SetSortOrder(Expression<Func<TEntity, object>> keySelector, bool ascending = true)
    {
        SortAscending = ascending;
        SortExpression = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        return this;
    }

    /// <summary>
    /// Sets pagination.
    /// </summary>
    /// <param name="pageIndex">The page index.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>
    /// This instance with the updated pagination.
    /// </returns>
    public QueryConstraint<TEntity> SetPagination(int pageIndex, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageIndex, nameof(pageIndex));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize, nameof(pageSize));
        PageIndex = pageIndex;
        PageSize = pageSize;
        return this;
    }
}
