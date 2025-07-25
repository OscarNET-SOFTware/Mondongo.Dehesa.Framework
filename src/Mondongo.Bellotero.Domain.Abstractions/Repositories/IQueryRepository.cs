// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryRepository.cs" company="OscarNET-SOFTware">
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
using Mondongo.Bellotero.Domain.Specifications;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Defines the contract for query operations (read-only) on an aggregate root entity.
/// </summary>
/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
public interface IQueryRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// Asynchronously checks whether one or more entities match a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous check operation.
    /// Its result contains a <c>true</c> value if one or more entities meet the specified criteria.
    /// </returns>
    Task<bool> AnyAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously counts the entities.
    /// </summary>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous counting operation.
    /// Its result contains the total number of entities.
    /// </returns>
    Task<long> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously counts the entities that match a given specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous counting operation.
    /// Its result contains the total number of entities that meet the specified specification.
    /// </returns>
    Task<long> CountAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously checks if exists a given entity.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous check operation.
    /// Its result contains a <c>true</c> value if the entity exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(object entityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the first entity that matches a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the first entity found, if it meets the specified criteria.
    /// </returns>
    Task<Maybe<TAggregateRoot>> FirstAsync(ISpecification<TAggregateRoot> specification,
                                           CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all entities that match a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains a sequence of entities that meet the specified criteria.
    /// </returns>
    Task<IEnumerable<TAggregateRoot>> GetAllAsync(ISpecification<TAggregateRoot> specification,
                                                  CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the found entity, if one exists.
    /// </returns>
    Task<Maybe<TAggregateRoot>> GetByIdAsync(object entityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the entities that match a given criteria.
    /// </summary>
    /// <param name="queryConstraint">The query constraint.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A paged collection containing the entities that match the specified criteria.
    /// </returns>
    Task<PagedCollection<TAggregateRoot>> GetPagedAsync(QueryConstraint<TAggregateRoot> queryConstraint,
                                                        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the unique entity that matches a given criteria.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous query operation.
    /// Its result contains the unique entity found, if it meets the specified criteria.
    /// </returns>
    Task<Maybe<TAggregateRoot>> SingleAsync(ISpecification<TAggregateRoot> specification,
                                           CancellationToken cancellationToken = default);
}
