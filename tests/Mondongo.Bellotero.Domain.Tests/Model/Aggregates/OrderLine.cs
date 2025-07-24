// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderLine.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Bellotero.Domain.Model.Aggregates;

public class OrderLine : EntityBase
{
    protected internal OrderLine(long id, long productId, int quantity, decimal unitPrice)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    protected OrderLine()
        : base()
    {
        // Parameterless constructor required by ORM.
    }

    public virtual long ProductId { get; protected set; }
    public virtual int Quantity { get; protected set; }
    public virtual decimal UnitPrice { get; protected set; }

    protected internal event Action<OrderLine, int>? QuantityChanged;

    public virtual void ChangeQuantity(int newQuantity)
    {
        if (Quantity != newQuantity)
        {
            Quantity = newQuantity;
            QuantityChanged?.Invoke(this, newQuantity);
        }
    }
}
