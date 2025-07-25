// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="OscarNET-SOFTware">
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
using Mondongo.Bellotero.Domain.Repositories;
using Mondongo.Bellotero.Domain.Specifications;

namespace Mondongo.Bellotero.Domain.Extensions;

/// <summary>
/// Provides several extension methods to extend the queryable functionality.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Creates a <see cref="PagedCollection{TEntity}" /> from a given <see cref="IQueryable{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="sourceQueryable">The source queryable.</param>
    /// <param name="queryConstraint">The query constraint.</param>
    /// <returns>
    /// A <see cref="PagedCollection{TEntity}" /> object that contains the paginated collection of entities.
    /// </returns>
    public static PagedCollection<TEntity> ToPagedCollection<TEntity>(this IQueryable<TEntity> sourceQueryable,
                                                                      QueryConstraint<TEntity> queryConstraint)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(queryConstraint);
        ArgumentNullException.ThrowIfNull(sourceQueryable);

        IQueryable<TEntity> constrainedQueryable = ApplySpecification(sourceQueryable, queryConstraint.Specification);
        long totalCount = GetTotalCount(constrainedQueryable);
        constrainedQueryable = ApplySorting(constrainedQueryable, queryConstraint);
        constrainedQueryable = ApplyPaging(constrainedQueryable, queryConstraint);
        IEnumerable<TEntity> entities = constrainedQueryable.ToList();

        return new PagedCollection<TEntity>(entities, totalCount, queryConstraint.PageIndex, queryConstraint.PageSize);
    }

    private static IQueryable<TEntity> ApplyPaging<TEntity>(IQueryable<TEntity> queryable,
                                                            QueryConstraint<TEntity> constraint)
        where TEntity : IEntity
            => queryable
                .Skip((constraint.PageIndex - 1) * constraint.PageSize)
                .Take(constraint.PageSize);

    private static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> queryable,
                                                             QueryConstraint<TEntity> constraint)
        where TEntity : IEntity
            => constraint.SortAscending
                ? queryable.OrderBy(constraint.SortExpression)
                : queryable.OrderByDescending(constraint.SortExpression);

    private static IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> source,
                                                                   ISpecification<TEntity> specification)
        where TEntity : IEntity
            => source.Where(specification.ToExpression());

    private static long GetTotalCount<TEntity>(IQueryable<TEntity> queryable)
        where TEntity : IEntity
            => queryable.Select(entity => entity.Id).LongCount();
}
