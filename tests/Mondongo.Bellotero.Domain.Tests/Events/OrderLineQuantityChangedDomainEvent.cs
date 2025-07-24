// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderLineQuantityChangedDomainEvent.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Events;

public class OrderLineQuantityChangedDomainEvent(long orderId, long orderLineId, int newQuantity) : DomainEventBase
{
    public long OrderId { get; } = orderId;
    public long OrderLineId { get; } = orderLineId;
    public int NewQuantity { get; } = newQuantity;
}
