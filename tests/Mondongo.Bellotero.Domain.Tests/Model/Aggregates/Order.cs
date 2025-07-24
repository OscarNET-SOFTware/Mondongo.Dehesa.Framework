// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Order.cs" company="OscarNET-SOFTware">
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

using System.Reflection;

using Mondongo.Bellotero.Domain.Events;
using Mondongo.Bellotero.Domain.Model.Entities;

namespace Mondongo.Bellotero.Domain.Model.Aggregates;

public class Order : AggregateRootBase
{
    private readonly List<OrderLine> _orderLines = [];

    public Order(long id, DateTime orderDate)
        : base(id)
    {
        OrderDate = orderDate;
    }

    protected Order()
        : base()
    {
        // Parameterless constructor required by ORM.
    }

    public virtual DateTime OrderDate { get; protected set; }

    public virtual IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

    public virtual void AddOrderLine(long id, long productId, int quantity, decimal unitPrice)
    {
        var line = new OrderLine(id, productId, quantity, unitPrice);
        line.QuantityChanged += OnOrderLineQuantityChanged;
        _orderLines.Add(line);
        AddDomainEvent(new OrderLineAddedDomainEvent(Id, line.Id));
    }

    public virtual void ChangeOrderDate(DateTime newDate)
    {
        OrderDate = newDate;
        AddDomainEvent(new OrderDateChangedDomainEvent(Id, newDate));
    }

    public new virtual void ClearDomainEvents() => base.ClearDomainEvents();

    public static Order Empty()  // ORM simulation :)
        => (Order)typeof(Order).GetConstructor(
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            types: Type.EmptyTypes,
            modifiers: null)
        !.Invoke(null);

    private void OnOrderLineQuantityChanged(OrderLine line, int newQuantity)
        => AddDomainEvent(new OrderLineQuantityChangedDomainEvent(Id, line.Id, newQuantity));
}
