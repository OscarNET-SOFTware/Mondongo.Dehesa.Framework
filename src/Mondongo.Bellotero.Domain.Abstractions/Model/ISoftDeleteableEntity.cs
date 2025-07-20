// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ISoftDeleteableEntity.cs" company="OscarNET-SOFTware">
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
/// Defines the contract for a soft deleteable entity.
/// </summary>
public interface ISoftDeleteableEntity
{
    /// <summary>
    /// Gets a value indicating whether this entity is softdeleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this entity is soft deleted; otherwise, <c>false</c>.
    /// </value>
    bool IsDeleted { get; }

    /// <summary>
    /// Gets the UTC timestamp when this entity was soft deleted.
    /// </summary>
    /// <value>
    ///   A <see cref="DateTime" /> object whose value is the UTC timestamp when this entity was soft deleted.
    /// </value>
    DateTime? DeletedOnUtc { get; }

    /// <summary>
    /// Marks this entity as soft deleted.
    /// </summary>
    void Delete();

    /// <summary>
    /// Restores the entity from a soft deleted state.
    /// </summary>
    void Restore();
}
