// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EquatableEntityTests.cs" company="OscarNET-SOFTware">
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

public sealed class EquatableEntityTests
{
    [Fact]
    public void Derived_entities_are_not_equal()
    {
        Employee sutA = EmployeeFactory.New(1L);
        DerivedEmployee sutB = DerivedEmployeeFactory.New(1L);

        bool equalsA = sutA.Equals(sutB);
        bool equalsB = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.False(equalsA);
        Assert.False(equalsB);
        Assert.True(notEquals);
    }

    [Fact]
    public void Entities_of_distinct_types_are_not_equal()
    {
        long sameId = 1L;
        object sutA = EmployeeFactory.New(sameId);
        object sutB = CustomerFactory.New(sameId);

        bool equalsA = sutA.Equals(sutB);
        bool equalsB = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.False(equalsA);
        Assert.False(equalsB);
        Assert.True(notEquals);
    }

    [Fact]
    public void Two_entities_with_the_same_identifier_are_equal()
    {
        long sameId = 1L;
        Employee sutA = EmployeeFactory.New(sameId);
        Employee sutB = EmployeeFactory.New(sameId);

        bool equalsA = sutA.Equals(sutB);
        bool equalsB = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.True(equalsA);
        Assert.True(equalsB);
        Assert.False(notEquals);
    }

    [Fact]
    public void Two_entities_with_the_distinct_identifier_are_not_equal()
    {
        Employee sutA = EmployeeFactory.New(1L);
        Employee sutB = EmployeeFactory.New(2L);

        bool equalsA = sutA.Equals(sutB);
        bool equalsB = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.False(equalsA);
        Assert.False(equalsB);
        Assert.True(notEquals);
    }

    [Fact]
    public void Two_entities_with_default_identifiers_are_not_equal()
    {
        Employee sutA = EmployeeFactory.New(default);
        Employee sutB = EmployeeFactory.New(default);

        bool equalsA = sutA.Equals(sutB);
        bool equalsB = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.False(equalsA);
        Assert.False(equalsB);
        Assert.True(notEquals);
    }

    [Fact]
    public void Two_entities_with_nullable_id_are_not_equal()
    {
        Employee sutA = EmployeeFactory.Empty();
        Employee sutB = EmployeeFactory.Empty();

        bool equals = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.False(equals);
        Assert.True(notEquals);
    }

    [Fact]
    public void Two_nullable_entities_are_equal()
    {
        Employee? sutA = null;
        Employee? sutB = null;

        bool equals = sutA == sutB;
        bool notEquals = sutA != sutB;

        Assert.True(equals);
        Assert.False(notEquals);
    }

    [Fact]
    public void Non_nullable_entity_and_nullable_entity_are_not_equal()
    {
        Employee sutA = EmployeeFactory.New(1L);
        Employee? sutB = null;

        Assert.NotNull(sutA);
        Assert.Null(sutB);
        Assert.False(sutA.Equals(null));
        Assert.False(sutA == sutB);
        Assert.False(sutB == sutA);
        Assert.True(sutA != sutB);
        Assert.True(sutB != sutA);
    }

    [Fact]
    public void Same_entity_referenced_multiple_times_is_the_same()
    {
        Employee sutA = EmployeeFactory.New(1L);
        Employee sutB = sutA;
        Employee sutC = sutB;

        Assert.True(sutA == sutB);
        Assert.True(sutB == sutC);
        Assert.False(sutA != sutB);
        Assert.False(sutB != sutC);
    }
}
