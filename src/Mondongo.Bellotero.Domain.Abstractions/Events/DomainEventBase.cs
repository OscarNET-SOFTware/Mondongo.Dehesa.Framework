// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEventBase.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Events;

/// <summary>
/// Represents the base class for a domain event.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class DomainEventBase : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventBase" /> class.
    /// </summary>
    protected DomainEventBase()
    {
    }

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay
        => $"({GetType().Name}) => Id : {Id:B} | OccurredOnUtc : {OccurredOnUtc:yyyy-MM-dd HH:mm:ss}";

    /// <summary>
    /// The identifier of this domain event.
    /// </summary>
    /// <value>
    ///   A <see cref="Guid" /> object whose value is the identifier of this domain event.
    /// </value>
    public Guid Id => Guid.NewGuid();

    /// <summary>
    /// Gets the UTC timestamp when this domain event occurred.
    /// </summary>
    /// <value>
    ///   A <see cref="DateTime" /> object whose value is the UTC timestamp when this domain event occurred.
    /// </value>
    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
