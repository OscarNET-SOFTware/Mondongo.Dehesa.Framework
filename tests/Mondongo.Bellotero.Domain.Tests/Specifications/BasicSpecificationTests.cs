// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicSpecificationTests.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Model.Aggregates;

namespace Mondongo.Bellotero.Domain.Specifications;

public sealed class BasicSpecificationTests
{
    [Fact]
    public void AndSpecification_should_return_true_when_both_specifications_are_true()
    {
        var order = new Order(1L, DateTime.Today);
        order.AddOrderLine(1L, 100L, 3, 1.50M);
        order.AddOrderLine(2L, 200L, 1, 5.75M);
        order.AddOrderLine(3L, 300L, 10, .99M);
        var hasLines = new OrderHasLinesSpecification();
        var allLinesHavePositiveQuantity = new AllOrderLinesHavePositiveQuantitySpecification();

        ISpecification<Order> sut = hasLines.And(allLinesHavePositiveQuantity);

        Assert.True(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void AndSpecification_should_return_false_when_at_least_one_specification_is_false()
    {
        var order = new Order(1L, DateTime.Today);
        order.AddOrderLine(1L, 100L, 3, 1.50M);
        order.AddOrderLine(2L, 200L, -1, 5.75M);
        order.AddOrderLine(3L, 300L, 0, .99M);
        var hasLines = new OrderHasLinesSpecification();
        var allLinesHavePositiveQuantity = new AllOrderLinesHavePositiveQuantitySpecification();

        ISpecification<Order> sut = hasLines.And(allLinesHavePositiveQuantity);

        Assert.False(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void OrSpecification_should_return_true_when_at_least_one_specification_is_true()
    {
        var order = new Order(1L, DateTime.Today);
        order.AddOrderLine(1L, 100L, 3, 1.50M);
        order.AddOrderLine(2L, 200L, -1, 5.75M);
        order.AddOrderLine(3L, 300L, 0, .99M);
        var hasLines = new OrderHasLinesSpecification();
        var allLinesHavePositiveQuantity = new AllOrderLinesHavePositiveQuantitySpecification();

        ISpecification<Order> sut = hasLines.Or(allLinesHavePositiveQuantity);

        Assert.True(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void OrSpecification_should_return_false_when_both_specifications_are_false()
    {
        var order = Order.Empty();
        var hasLines = new OrderHasLinesSpecification();
        var someLinesHaveZeroUnitPrice = new SomeOrderLinesHaveZeroUnitPriceSpecfication();

        ISpecification<Order> sut = hasLines.Or(someLinesHaveZeroUnitPrice);

        Assert.False(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void NotSpecification_should_return_true_when_specification_is_false()
    {
        var order = Order.Empty();
        var hasLines = new OrderHasLinesSpecification();

        ISpecification<Order> sut = hasLines.Not();

        Assert.True(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void NotSpecification_should_return_false_when_specification_is_true()
    {
        var order = Order.Empty();
        var alwaysTrue = new AlwaysTrueSpecification<Order>();

        ISpecification<Order> sut = alwaysTrue.Not();

        Assert.False(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void Complex_specification_should_return_true_when_condition_is_met()
    {
        var order = new Order(1L, DateTime.Today);
        order.AddOrderLine(1L, 100L, 3, 1.50M);
        order.AddOrderLine(2L, 200L, -1, 5.75M);
        order.AddOrderLine(3L, 300L, 0, .99M);
        order.AddOrderLine(4L, 400L, 5, decimal.Zero);
        var hasLines = new OrderHasLinesSpecification();
        var allLinesHavePositiveQuantity = new AllOrderLinesHavePositiveQuantitySpecification();
        var someLinesHaveZeroUnitPrice = new SomeOrderLinesHaveZeroUnitPriceSpecfication();

        ISpecification<Order> sut = hasLines.And(allLinesHavePositiveQuantity.Or(someLinesHaveZeroUnitPrice));

        Assert.True(sut.IsSatisfiedBy(order));
    }

    [Fact]
    public void ByIdSpecification_should_return_true_when_identifier_matches()
    {
        var order = new Order(1L, DateTime.Today);

        var sut = new ByIdSpecification<Order>(1L);

        Assert.True(sut.IsSatisfiedBy(order));
    }
}
