// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableEntityTests.cs" company="OscarNET-SOFTware">
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

using CSharpFunctionalExtensions;

using Mondongo.Bellotero.Domain.Model.Entities;

namespace Mondongo.Bellotero.Domain.Model;

public sealed class ValidatableEntityTests
{
    [Fact]
    public void Validate_returns_successful_validation_for_valid_entity()
    {
        Employee sut = EmployeeFactory.New(1L);

        Assert.True(sut.Validate().IsSuccess);
    }

    [Fact]
    public void Validate_returns_failed_validation_for_invalid_entity()
    {
        Employee sut = EmployeeFactory.Empty();

        Result sutResult = sut.Validate();

        Assert.True(sutResult.IsFailure);
        Assert.Equal(Employee.IsMandatoryMessageForFullName, sutResult.Error);
    }
}
