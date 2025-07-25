// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="OscarNET-SOFTware">
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
/// Specification that represents a conditional AND operation that evaluates the second operand
/// only if the first operand evaluates to <c>true</c>.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <param name="left">The left specification.</param>
/// <param name="right">The right specification.</param>
internal sealed class AndSpecification<TEntity>(ISpecification<TEntity> left, ISpecification<TEntity> right) : SpecificationBase<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Stores the left specification.
    /// </summary>
    private readonly ISpecification<TEntity> _left = left;

    /// <summary>
    /// Stores the right specification.
    /// </summary>
    private readonly ISpecification<TEntity> _right = right;

    /// <summary>
    /// Converts this specification to an expression.
    /// </summary>
    /// <returns>
    /// The converted expression.
    /// </returns>
    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity));
        BinaryExpression body = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameterExpression),
            Expression.Invoke(rightExpression, parameterExpression)
        );

        return Expression.Lambda<Func<TEntity, bool>>(body, parameterExpression);
    }

    /// <summary>
    /// Returns a textual representation of this specification.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> object containing the textual representation of this specification.
    /// </returns>
    [ExcludeFromCodeCoverage]
    public override string ToString() => $"( {_left} AND {_right} ) ";
}
