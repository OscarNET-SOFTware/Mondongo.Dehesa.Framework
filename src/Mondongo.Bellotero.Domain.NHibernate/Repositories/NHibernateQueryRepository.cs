// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateQueryRepository.cs" company="OscarNET-SOFTware">
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

using CSharpFunctionalExtensions;

using Mondongo.Bellotero.Domain.Model;
using Mondongo.Bellotero.Domain.Specifications;

using NHibernate;
using NHibernate.Linq;

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Provides NHibernate's repository implementation for query operations (read-only) on an aggregate root entity.
/// </summary>
/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
public class NHibernateQueryRepository<TAggregateRoot> : IQueryRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    /// <summary>
    /// Stores the session.
    /// </summary>
    protected readonly ISession Session;

    /// <summary>
    /// Initializes a new instance of the <see cref="NHibernateQueryRepository{TAggregateRoot}" /> class.
    /// </summary>
    /// <param name="session">The session.</param>
    public NHibernateQueryRepository(ISession session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));
        Session = session;
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
    public Task<bool> AnyAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));

        var predicate = specification.ToExpression();
        return Session.Query<TAggregateRoot>().Where(predicate).AnyAsync(cancellationToken);
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
        => Session.Query<TAggregateRoot>().LongCountAsync(cancellationToken);

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

        var predicate = specification.ToExpression();
        return Session.Query<TAggregateRoot>().Where(predicate).LongCountAsync(cancellationToken);
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
    public Task<bool> ExistsAsync(object entityId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entityId, nameof(entityId));

        var specification = new ByIdSpecification<TAggregateRoot>(entityId);
        var predicate = specification.ToExpression();
        return Session.Query<TAggregateRoot>().Where(predicate).AnyAsync(cancellationToken);
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

        var predicate = specification.ToExpression();
        return await Session.Query<TAggregateRoot>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
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

        var predicate = specification.ToExpression();
        return await Session.Query<TAggregateRoot>().Where(predicate).ToListAsync(cancellationToken);
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
        return await Session.GetAsync<TAggregateRoot>(entityId, cancellationToken);
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

        var predicate = queryConstraint.Specification.ToExpression();
        IQueryable<TAggregateRoot> query = Session.Query<TAggregateRoot>().Where(predicate);

        long totalCount = await query.LongCountAsync(cancellationToken);

        query = queryConstraint.SortAscending
            ? query.OrderBy(queryConstraint.SortExpression)
            : query.OrderByDescending(queryConstraint.SortExpression);

        IEnumerable<TAggregateRoot> entities = await query
            .Skip((queryConstraint.PageIndex - 1) * queryConstraint.PageSize)
            .Take(queryConstraint.PageSize)
            .ToListAsync(cancellationToken);

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

        var predicate = specification.ToExpression();
        return await Session.Query<TAggregateRoot>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
    }
}
