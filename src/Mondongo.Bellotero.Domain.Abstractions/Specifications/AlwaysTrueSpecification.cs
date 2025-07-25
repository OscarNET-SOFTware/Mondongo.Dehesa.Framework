// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AlwaysTrueSpecification.cs" company="OscarNET-SOFTware">
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

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Specifications;

/// <summary>
/// Specification that represents a TRUE condition.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public sealed class AlwaysTrueSpecification<TEntity> : SpecificationBase<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Converts this specification to an expression.
    /// </summary>
    /// <returns>
    /// The converted expression.
    /// </returns>
    public override Expression<Func<TEntity, bool>> ToExpression() => _ => true;

    /// <summary>
    /// Returns a textual representation of this specification.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> object containing the textual representation of this specification.
    /// </returns>
    [ExcludeFromCodeCoverage]
    public override string ToString() => $"{bool.TrueString.ToUpperInvariant()} ";
}
