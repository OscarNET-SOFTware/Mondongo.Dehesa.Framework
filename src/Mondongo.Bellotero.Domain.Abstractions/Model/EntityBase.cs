// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="OscarNET-SOFTware">
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

using System.Diagnostics;

using Mondongo.Bellotero.Domain.Extensions;

namespace Mondongo.Bellotero.Domain.Model;

/// <summary>
/// Represents the base class for an entity that is soft deleteable and validatable,
/// with a <see cref="long" /> type as identifier.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class EntityBase : EntityBase<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase" /> class.
    /// </summary>
    protected EntityBase()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase" /> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected EntityBase(long id)
        : base(id)
    {
    }

    private string DebuggerDisplay => IsTransient
        ? $"({this.GetUnproxiedType().Name}) => {nameof(IsTransient)} | HashCode : {GetHashCode()}"
        : $"({this.GetUnproxiedType().Name}) => {nameof(Id)} : {Id} | HashCode : {GetHashCode()}";
}

/// <summary>
/// Represents the base class for an entity that is soft deleteable and validatable.
/// </summary>
/// <typeparam name="TId">The type of entity identifier.</typeparam>
public abstract class EntityBase<TId> : IComparable, IComparable<EntityBase<TId>>, IEntity, IEquatable<EntityBase<TId>>, ISoftDeleteableEntity, IValidatableEntity
    where TId : IComparable, IComparable<TId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase{TId}" /> class.
    /// </summary>
    protected EntityBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase{TId}" /> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected EntityBase(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public virtual TId Id { get; protected set; } = default!;

    /// <summary>
    /// Gets the underlying identifier.
    /// </summary>
    /// <value>
    /// An <see cref="object" /> that represents the underlying identifier.
    /// </value>
    object IEntity.Id => Id;

    /// <summary>
    /// Gets a value indicating whether this entity is transient.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this entity is transient; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsTransient => Id is null || Id.Equals(default(TId));

    /// <summary>
    /// Gets or sets a value indicating whether this entity is softdeleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this entity is soft deleted; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsDeleted { get; protected set; }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(EntityBase<TId>? first, EntityBase<TId>? second)
        => !(first == second);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(EntityBase<TId>? first, EntityBase<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    /// <summary>
    /// Implements the operator <![CDATA[<]]>.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator <(EntityBase<TId>? first, EntityBase<TId>? second)
        => first is null ? second is not null : first.CompareTo(second) < 0;

    /// <summary>
    /// Implements the operator <![CDATA[>]]>.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator >(EntityBase<TId>? first, EntityBase<TId>? second)
        => first is not null && first.CompareTo(second) > 0;

    /// <summary>
    /// Implements the operator <![CDATA[<=]]>.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator <=(EntityBase<TId>? first, EntityBase<TId>? second)
        => first is null || first.CompareTo(second) <= 0;

    /// <summary>
    /// Implements the operator <![CDATA[>=]]>.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator >=(EntityBase<TId>? first, EntityBase<TId>? second)
        => first is not null && first.CompareTo(second) >= 0;

    /// <summary>
    /// Indicates whether this entity is equal to another entity of the same type.
    /// </summary>
    /// <param name="other">An entity to compare with this entity.</param>
    /// <returns>
    ///   <c>true</c> if this entity is equal to the <paramref name="other">other</paramref> entity; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool Equals(EntityBase<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetUnproxiedType() != other.GetUnproxiedType())
        {
            return false;
        }

        if (IsTransient || other.IsTransient)
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this entity.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this entity.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="object" /> is equal to this entity; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj) => Equals(obj as EntityBase<TId>);

    /// <summary>
    /// Returns a hash code for this entity.
    /// </summary>
    /// <returns>
    /// A hash code for this entity, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode() => (this.GetUnproxiedType().ToString() + Id).GetHashCode();

    /// <summary>
    /// Gets or sets the UTC timestamp when this entity was soft deleted.
    /// </summary>
    /// <value>
    ///   A <see cref="DateTime" /> object whose value is the UTC timestamp when this entity was soft deleted.
    /// </value>
    public virtual DateTime? DeletedOnUtc { get; protected set; }

    /// <summary>
    /// Marks this entity as soft deleted.
    /// </summary>
    public virtual void Delete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletedOnUtc = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Restores the entity from a soft deleted state.
    /// </summary>
    public virtual void Restore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletedOnUtc = null;
        }
    }

    /// <summary>
    /// Validates this entity.
    /// </summary>
    /// <returns>
    /// The validation result.
    /// </returns>
    public virtual Result Validate() => Result.Success();

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer
    /// that indicates whether the current instance precedes, follows, or occurs in the same position
    /// in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// The return value has these meanings:
    ///   <c>Less than zero</c> - This instance precedes <paramref name="obj" /> in the sort order.
    ///   <c>Zero</c> - This instance occurs in the same position in the sort order as <paramref name="obj" />.
    ///   <c>Greater than zero</c> - This instance follows <paramref name="obj" /> in the sort order.
    /// </returns>
    public virtual int CompareTo(object? obj) => CompareTo(obj as EntityBase<TId>);

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer
    /// that indicates whether the current instance precedes, follows, or occurs in the same position
    /// in the sort order as the other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// The return value has these meanings:
    ///   <c>Less than zero</c> - This instance precedes <paramref name="other" /> in the sort order.
    ///   <c></c> - This instance occurs in the same position in the sort order as <paramref name="other" />.
    ///   <c></c> - This instance follows <paramref name="other" /> in the sort order.
    /// </returns>
    public virtual int CompareTo(EntityBase<TId>? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (Id is null && other.Id is null)
        {
            return 0;
        }

        if (Id is null)
        {
            return -1;
        }

        if (other.Id is null)
        {
            return 1;
        }

        return Id.CompareTo(other.Id);
    }
}
