// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedCollection.cs" company="OscarNET-SOFTware">
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

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Represents a paginated collection of entities.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="PagedCollection{TEntity}" /> class.
/// </remarks>
/// <param name="entities">The entities.</param>
/// <param name="totalCount">The total count.</param>
/// <param name="pageIndex">The index of the page.</param>
/// <param name="pageSize">The size of the page.</param>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class PagedCollection<TEntity>(IEnumerable<TEntity> entities,
                                             long totalCount,
                                             int pageIndex,
                                             int pageSize) : IEnumerable<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Stores the list of entities.
    /// </summary>
    private readonly List<TEntity> _entities = [.. entities ?? throw new ArgumentNullException(nameof(entities))];

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay
        => $"({typeof(TEntity).Name}) => Page {PageIndex:#,##0} of {TotalPages:#,##0} | {nameof(TotalCount)} : {TotalCount:#,##0}";

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <value>
    /// A read-only list of domain events.
    /// </value>
    public IReadOnlyList<TEntity> Entities => _entities.AsReadOnly();

    /// <summary>
    /// Gets a value indicating whether this instance has next page.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
    /// </value>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Gets a value indicating whether this instance has previous page.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
    /// </value>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// Gets the index of the page.
    /// </summary>
    /// <value>
    /// The index of the page.
    /// </value>
    public int PageIndex { get; } = EnsureIsGreaterThanZero(pageIndex);

    /// <summary>
    /// Gets the size of the page.
    /// </summary>
    /// <value>
    /// The size of the page.
    /// </value>
    public int PageSize { get; } = EnsureIsGreaterThanZero(pageSize);

    /// <summary>
    /// Gets the total count.
    /// </summary>
    /// <value>
    /// The total count.
    /// </value>
    public long TotalCount { get; } = EnsureIsGreaterThanOrEqualToZero(totalCount);

    /// <summary>
    /// Gets the total pages;
    /// </summary>
    /// <value>
    /// The total pages.
    /// </value>
    public int TotalPages { get; } = CalculateTotalPages(totalCount, pageSize);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<TEntity> GetEnumerator() => _entities.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Calculates the total pages.
    /// </summary>
    /// <returns>
    /// The total number of pages.
    /// </returns>
    private static int CalculateTotalPages(long totalCount, int pageSize)
    {
        int totalPages = (int)(totalCount / pageSize);
        if ((totalCount % pageSize) > 0)
        {
            totalPages++;
        }

        return totalPages;
    }

    private static long EnsureIsGreaterThanOrEqualToZero(long number)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(number, 0L);
        return number;
    }

    private static int EnsureIsGreaterThanZero(int number)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(number, 0L);
        return number;
    }
}
