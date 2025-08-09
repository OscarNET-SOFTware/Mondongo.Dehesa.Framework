// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ByIdSpecification.cs" company="OscarNET-SOFTware">
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
/// Initializes a new instance of the <see cref="ByIdSpecification{TEntity}" /> class.
/// </summary>
/// <param name="entityId">The entity identifier.</param>
/// <typeparam name="TEntity">The entity type.</typeparam>
public sealed class ByIdSpecification<TEntity>(object entityId) : SpecificationBase<TEntity>()
    where TEntity : IEntity
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    /// <value>
    /// An <see cref="object" /> that represents the entity identifier.
    /// </value>
    public object EntityId { get; } = entityId ?? throw new ArgumentNullException(nameof(entityId));

    /// <summary>
    /// Converts this specification to an expression.
    /// </summary>
    /// <returns>
    /// The converted expression.
    /// </returns>
    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "entity");
        MemberExpression idProperty = Expression.PropertyOrField(parameterExpression, nameof(IEntity.Id));
        Type idType = idProperty.Type;
        ConstantExpression idConstant = Expression.Constant(Convert.ChangeType(EntityId, idType), idType);

        BinaryExpression equal = Expression.Equal(idProperty, idConstant);
        return Expression.Lambda<Func<TEntity, bool>>(equal, parameterExpression);
    }
}
