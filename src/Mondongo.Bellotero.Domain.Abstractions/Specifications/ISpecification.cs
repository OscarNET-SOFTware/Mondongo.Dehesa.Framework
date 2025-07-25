// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="OscarNET-SOFTware">
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

using System.Linq.Expressions;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Specifications;

/// <summary>
/// Defines the contract for a specification.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public interface ISpecification<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Determines whether this specification is satisfied by the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>
    ///   <c>true</c> if this specification is satisfied by the specified entity; otherwise, <c>false</c>.
    /// </returns>
    bool IsSatisfiedBy(TEntity entity);

    /// <summary>
    /// Applies the condition AND operation.
    /// </summary>
    /// <param name="other">The other specification.</param>
    /// <returns>
    /// A <see cref="AndSpecification{TEntity}" /> object.
    /// </returns>
    ISpecification<TEntity> And(ISpecification<TEntity> other);

    /// <summary>
    /// Applies the conditional OR operation.
    /// </summary>
    /// <param name="other">The other specification.</param>
    /// <returns>
    /// A <see cref="OrSpecification{TEntity}" /> object.
    /// </returns>
    ISpecification<TEntity> Or(ISpecification<TEntity> other);

    /// <summary>
    /// Applies the condition NOT operation.
    /// </summary>
    /// <returns>
    /// A <see cref="NotSpecification{TEntity}" /> object.
    /// </returns>
    ISpecification<TEntity> Not();

    /// <summary>
    /// Converts this specification to an expression.
    /// </summary>
    /// <returns>
    /// The converted expression.
    /// </returns>
    Expression<Func<TEntity, bool>> ToExpression();
}
