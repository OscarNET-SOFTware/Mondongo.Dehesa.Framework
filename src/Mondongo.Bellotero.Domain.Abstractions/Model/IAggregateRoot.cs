// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Events;

namespace Mondongo.Bellotero.Domain.Model;

/// <summary>
/// Defines the contract for an aggregate root.
/// </summary>
public interface IAggregateRoot : IEntity
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <value>
    /// A read-only list of domain events.
    /// </value>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Gets the version that it's used by the ORM
    /// for checking concurrency conflicts (Optimistic Control).
    /// </summary>
    /// <value>
    /// A long value that represents the version.
    /// </value>
    long Version { get; }
}
