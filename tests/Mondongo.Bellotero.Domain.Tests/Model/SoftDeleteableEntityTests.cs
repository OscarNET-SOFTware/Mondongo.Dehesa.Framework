// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SoftDeleteableEntityTests.cs" company="OscarNET-SOFTware">
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

public sealed class SoftDeleteableEntityTests
{
    [Fact]
    public void Delete_marks_entity_as_deleted()
    {
        Employee sut = EmployeeFactory.New(1L);

        sut.Delete();

        Assert.True(sut.IsDeleted);
        Assert.NotNull(sut.DeletedOnUtc);
    }

    [Fact]
    public void Restore_reverts_a_deleted_entity()
    {
        Employee sut = EmployeeFactory.NewDeleted();

        sut.Restore();

        Assert.False(sut.IsDeleted);
        Assert.Null(sut.DeletedOnUtc);
    }

    [Fact]
    public void Delete_does_not_change_entity_if_it_has_already_been_deleted()
    {
        Employee sut = EmployeeFactory.NewDeleted();
        DateTime? expectedDeletedOnUtc = sut.DeletedOnUtc;

        sut.Delete();

        Assert.True(sut.IsDeleted);
        Assert.Equal(expectedDeletedOnUtc, sut.DeletedOnUtc);
    }
}
