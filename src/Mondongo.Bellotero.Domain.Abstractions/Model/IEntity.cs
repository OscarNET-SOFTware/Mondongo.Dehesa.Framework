// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Model;

/// <summary>
/// Defines the contract for an entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the underlying identifier.
    /// </summary>
    /// <value>
    /// An <see cref="object" /> that represents the underlying identifier.
    /// </value>
    object Id { get; }

    /// <summary>
    /// Gets a value indicating whether this entity is transient.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this entity is transient; otherwise, <c>false</c>.
    /// </value>
    bool IsTransient { get; }
}
