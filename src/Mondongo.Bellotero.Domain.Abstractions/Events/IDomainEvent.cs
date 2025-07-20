// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEvent.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Events;

/// <summary>
/// Defines the contract for a domain event.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// The identifier of this domain event.
    /// </summary>
    /// <value>
    ///   A <see cref="Guid" /> object whose value is the identifier of this domain event.
    /// </value>
    Guid Id { get; }

    /// <summary>
    /// Gets the UTC timestamp when this domain event occurred.
    /// </summary>
    /// <value>
    ///   A <see cref="DateTime" /> object whose value is the UTC timestamp when this domain event occurred.
    /// </value>
    DateTime OccurredOnUtc { get; }
}
