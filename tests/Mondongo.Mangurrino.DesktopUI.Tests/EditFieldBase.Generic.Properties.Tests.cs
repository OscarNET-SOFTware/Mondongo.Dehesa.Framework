// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.Properties.Tests.cs" company="OscarNET-SOFTware">
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

namespace Mondongo.Mangurrino.DesktopUI;

public sealed partial class EditFieldBaseGenericTests
{
    [WpfFact]
    public void FieldValue_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        string value = "Hello world!";
        sut.FieldValue = value;

        Assert.Equal(value, sut.FieldValue);
    }

    [WpfFact]
    public void FieldValue_set_value_updates_field_text_as_expected()
    {
        var sut = new TestEditField();

        _ = sut.OnApplyTemplateSimulation();

        sut.FieldValue = "Hello world!";

        Assert.Equal(sut.FieldText, sut.ConvertFieldValueToEditableString(sut.FieldValue));
        Assert.Equal(sut._field!.Text, sut.FieldText);
    }
}
