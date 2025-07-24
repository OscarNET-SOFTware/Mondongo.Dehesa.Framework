// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicAggregateTests.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Events;
using Mondongo.Bellotero.Domain.Model.Aggregates;

namespace Mondongo.Bellotero.Domain.Model;

public sealed class BasicAggregateTests
{
    [Fact]
    public void Aggregate_instantiated_by_orm_has_no_domain_events()
    {
        var sut = Order.Empty();

        Assert.Empty(sut.DomainEvents);
    }

    [Fact]
    public void Aggregate_domain_event_has_an_identifier_and_when_occurred_on()
    {
        var sut = new Order(1L, DateTime.Now);

        DateTime newDate = DateTime.Now.AddDays(2);
        sut.ChangeOrderDate(newDate);

        OrderDateChangedDomainEvent sutEvent = sut.DomainEvents.OfType<OrderDateChangedDomainEvent>().Single();
        Assert.Equal(DateTime.UtcNow, sutEvent.OccurredOnUtc, TimeSpan.FromSeconds(3D));
        Assert.NotEqual(Guid.Empty, sutEvent.Id);
    }

    [Fact]
    public void AddOrderLine_should_add_order_line_and_raise_event()
    {
        var sut = new Order(1L, DateTime.Now);

        sut.AddOrderLine(1L, 15, 2, 25m);

        Assert.Single(sut.OrderLines);
        OrderLineAddedDomainEvent? sutEvent = sut.DomainEvents.OfType<OrderLineAddedDomainEvent>().FirstOrDefault();
        Assert.NotNull(sutEvent);
    }

    [Fact]
    public void ChangeOrderDate_should_raise_domain_event()
    {
        var sut = new Order(1L, DateTime.Now);

        DateTime newDate = DateTime.Now.AddDays(2);
        sut.ChangeOrderDate(newDate);

        OrderDateChangedDomainEvent? sutEvent = sut.DomainEvents.OfType<OrderDateChangedDomainEvent>().FirstOrDefault();
        Assert.NotNull(sutEvent);
        Assert.Equal(newDate, sutEvent.NewDate);
    }

    [Fact]
    public void ChangeOrderLineQuantity_should_raise_event_on_order()
    {
        var sut = new Order(1L, DateTime.Now);
        sut.AddOrderLine(1L, 10, 1, 5m);
        OrderLine sutLine = sut.OrderLines.First();

        sutLine.ChangeQuantity(5);

        OrderLineQuantityChangedDomainEvent? sutEvent = sut.DomainEvents.OfType<OrderLineQuantityChangedDomainEvent>().FirstOrDefault();
        Assert.NotNull(sutEvent);
        Assert.Equal(sutLine.Id, sutEvent.OrderLineId);
        Assert.Equal(5, sutEvent.NewQuantity);
    }

    [Fact]
    public void Clear_should_clear_all_domain_events()
    {
        var sut = new Order(1L, DateTime.Now);
        sut.ChangeOrderDate(DateTime.Now.AddDays(2));
        sut.AddOrderLine(1L, 10, 1, 5m);
        OrderLine sutLine = sut.OrderLines.First();
        sutLine.ChangeQuantity(5);

        Assert.Equal(3, sut.DomainEvents.Count);

        sut.ClearDomainEvents();

        Assert.Empty(sut.DomainEvents);
    }
}
