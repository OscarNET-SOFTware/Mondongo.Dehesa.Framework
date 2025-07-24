// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregateRootBase.cs" company="OscarNET-SOFTware">
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
using System.Diagnostics.CodeAnalysis;

using Mondongo.Bellotero.Domain.Events;
using Mondongo.Bellotero.Domain.Extensions;

namespace Mondongo.Bellotero.Domain.Model;

/// <summary>
/// Represents the base class for an entity that is aggregate root, soft deleteable and validatable,
/// with a <see cref="long" /> type as identifier.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class AggregateRootBase : AggregateRootBase<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRootBase" /> class.
    /// </summary>
    protected AggregateRootBase()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRootBase" /> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected AggregateRootBase(long id)
        : base(id)
    {
    }

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => IsTransient
        ? $"({this.GetUnproxiedType().Name}) => {nameof(IsTransient)} | {nameof(Version)} : {Version} | HashCode : {GetHashCode()}"
        : $"({this.GetUnproxiedType().Name}) => {nameof(Id)} : {Id} | {nameof(Version)} : {Version} | HashCode : {GetHashCode()}";
}

/// <summary>
/// Represents the base class for an entity that is aggregate root, soft deleteable and validatable.
/// </summary>
/// <typeparam name="TId">The type of entity identifier.</typeparam>
public abstract class AggregateRootBase<TId> : EntityBase<TId>, IAggregateRoot
    where TId : IComparable, IComparable<TId>
{
    /// <summary>
    /// Stores the list of domain events.
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRootBase{TId}" /> class.
    /// </summary>
    protected AggregateRootBase()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRootBase{TId}" /> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected AggregateRootBase(TId id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <value>
    /// A read-only list of domain events.
    /// </value>
    public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Gets or sets the version that it's used by the ORM
    /// for checking concurrency conflicts (Optimistic Control).
    /// </summary>
    /// <value>
    /// A long value that represents the version.
    /// </value>
    public virtual long Version { get; protected set; }

    /// <summary>
    /// Clears the list of domain events.
    /// </summary>
    protected virtual void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Adds a given domain event to the list of domain events.
    /// </summary>
    /// <param name="domainEvent">The domain event to add to the list of domain events.</param>
    protected virtual void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
