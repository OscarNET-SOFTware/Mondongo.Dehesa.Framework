// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ComparableEntityTests.cs" company="OscarNET-SOFTware">
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

using Mondongo.Bellotero.Domain.Model.Entities;

namespace Mondongo.Bellotero.Domain.Model;

public sealed class ComparableEntityTests
{
    [Fact]
    public void Entities_can_be_sorted()
    {
        Customer customer1 = CustomerFactory.New(1L);
        Customer customer2 = CustomerFactory.New(2L);
        Customer customer3 = CustomerFactory.New(3L);
        Customer customer4 = CustomerFactory.New(4L);
        Customer customer5 = CustomerFactory.New(5L);

        Customer?[] sutSortedCustomers = [.. new[] { customer5, customer1, null, customer4, customer2, customer3 }
            .OrderBy(customer => customer)];

        Assert.Null(sutSortedCustomers[0]);
        Assert.Same(customer1, sutSortedCustomers[1]);
        Assert.Same(customer2, sutSortedCustomers[2]);
        Assert.Same(customer3, sutSortedCustomers[3]);
        Assert.Same(customer4, sutSortedCustomers[4]);
        Assert.Same(customer5, sutSortedCustomers[5]);
    }

    [Fact]
    public void Non_nullable_entity_is_greater_than_nullable_entity()
    {
        Customer nonNullableCustomer = CustomerFactory.New(1L);
        Customer? nullableCustomer = null;

        bool isGreaterThan = nonNullableCustomer > nullableCustomer;
        bool isGreaterOrEqualTo = nonNullableCustomer >= nullableCustomer;
        bool isLessThan = nonNullableCustomer < nullableCustomer;
        bool isLessOrEqualTo = nonNullableCustomer <= nullableCustomer;

        Assert.True(isGreaterThan);
        Assert.True(isGreaterOrEqualTo);
        Assert.False(isLessThan);
        Assert.False(isLessOrEqualTo);
    }

    [Fact]
    public void Nullable_entity_is_less_than_non_nullable_entity()
    {
        Customer? nullableCustomer = null;
        Customer nonNullableCustomer = CustomerFactory.New(1L);

        bool isLessThan = nullableCustomer < nonNullableCustomer;
        bool isLessOrEqualTo = nullableCustomer <= nonNullableCustomer;
        bool isGreaterThan = nullableCustomer > nonNullableCustomer;
        bool isGreaterOrEqualTo = nullableCustomer >= nonNullableCustomer;

        Assert.True(isLessThan);
        Assert.True(isLessOrEqualTo);
        Assert.False(isGreaterThan);
        Assert.False(isGreaterOrEqualTo);
    }

    [Fact]
    public void Same_entity_referenced_multiple_times_has_the_same_comparison()
    {
        Customer sutA = CustomerFactory.New(1L);
        Customer sutB = sutA;
        Customer sutC = sutB;

        Assert.Equal(0, sutA.CompareTo(sutB));
        Assert.Equal(0, sutB.CompareTo(sutC));
    }

    [Fact]
    public void Entities_with_null_identifier_have_the_same_comparison()
    {
        AccountUser sutA = AccountUserFactory.Empty();
        AccountUser sutB = AccountUserFactory.Empty();

        Assert.Equal(0, sutA.CompareTo(sutB));
        Assert.Equal(0, sutB.CompareTo(sutA));
    }

    [Fact]
    public void If_one_entity_has_null_identifier_then_it_is_less_than()
    {
        AccountUser sutA = AccountUserFactory.Empty();
        AccountUser sutB = AccountUserFactory.New(new AccountUserId(Guid.NewGuid().ToString("B")));

        Assert.Equal(-1, sutA.CompareTo(sutB));
        Assert.Equal(1, sutB.CompareTo(sutA));
    }

    [Fact]
    public void Entities_with_distinct_identifier_types_are_comparatively_null()
    {
        AccountUser sutA = AccountUserFactory.New(new AccountUserId(Guid.NewGuid().ToString("B")));
        Customer sutB = CustomerFactory.New(1L);

        Assert.Equal(1, sutA.CompareTo(sutB));
        Assert.Equal(1, sutB.CompareTo(sutA));
    }
}
