// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DapperSpecificationVisitor.cs" company="OscarNET-SOFTware">
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

using Dapper;

using Mondongo.Bellotero.Domain.Model;

namespace Mondongo.Bellotero.Domain.Specifications;

/// <summary>
/// Translates a generic <see cref="ISpecification{TEntity}" /> (including nested AND/OR/NOT compositions)
/// into a SQL WHERE fragment and parameters for use with Dapper.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public sealed class DapperSpecificationVisitor<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Visits the given specification and converts it into a SQL WHERE fragment and parameters.
    /// </summary>
    /// <param name="specification">The specification to translate.</param>
    /// <returns>
    /// A tuple containing:
    ///   - SQL fragment (without the "WHERE" keyword)
    ///   - DynamicParameters object for Dapper
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Thrown when the specification type is not supported by this visitor.
    /// </exception>
    public (string Sql, DynamicParameters Params) Visit(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        if (specification is AlwaysTrueSpecification<TEntity>)
        {
            return ("1=1", new DynamicParameters());
        }

        if (specification is ByIdSpecification<TEntity> byIdSpec)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", byIdSpec.EntityId);
            return ("Id = @Id", parameters);
        }

        if (specification is AndSpecification<TEntity> andSpec)
        {
            (string leftSql, DynamicParameters leftParams) = Visit(andSpec.Left);
            (string rightSql, DynamicParameters rightParams) = Visit(andSpec.Right);

            string sql = $"({leftSql} AND {rightSql})";
            leftParams.AddDynamicParams(rightParams);
            return (sql, leftParams);
        }

        if (specification is OrSpecification<TEntity> orSpec)
        {
            (string leftSql, DynamicParameters leftParams) = Visit(orSpec.Left);
            (string rightSql, DynamicParameters rightParams) = Visit(orSpec.Right);

            string sql = $"({leftSql} OR {rightSql})";
            leftParams.AddDynamicParams(rightParams);
            return (sql, leftParams);
        }

        if (specification is NotSpecification<TEntity> notSpec)
        {
            (string innerSql, DynamicParameters innerParams) = Visit(notSpec.InnerSpecification);
            string sql = $"(NOT({innerSql}))";
            return (sql, innerParams);
        }

        throw new NotSupportedException(
            $"Specification type '{specification.GetType().Name}' is not supported by {nameof(DapperSpecificationVisitor<TEntity>)}.");
    }
}
