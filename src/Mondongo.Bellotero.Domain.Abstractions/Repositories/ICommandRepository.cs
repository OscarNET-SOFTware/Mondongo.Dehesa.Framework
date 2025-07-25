// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandRepository.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Repositories;

/// <summary>
/// Defines the contract for command operations (write) on an aggregate root entity.
/// </summary>
/// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
public interface ICommandRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// Asynchronously adds an entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous add operation.
    /// </returns>
    Task AddAsync(TAggregateRoot entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    Task DeleteAsync(TAggregateRoot entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous update operation.
    /// </returns>
    Task UpdateAsync(TAggregateRoot entity, CancellationToken cancellationToken = default);
}
