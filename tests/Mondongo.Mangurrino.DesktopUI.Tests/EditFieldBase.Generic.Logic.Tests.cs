// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.Logic.Tests.cs" company="OscarNET-SOFTware">
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

using System.Windows;

namespace Mondongo.Mangurrino.DesktopUI;

partial class EditFieldBaseGenericTests
{
    [WpfFact]
    public void FocusEvents_update_text_correctly()
    {
        var sut = new TestEditField();
        TestEditFieldTemplateParts templateParts = sut.OnApplyTemplateSimulation();

        sut.FieldValue = "Hello world!";
        string editableString = sut.ConvertFieldValueToEditableString(sut.FieldValue);

        sut.OnFieldGotFocus(templateParts.PART_Field, new RoutedEventArgs(UIElement.GotFocusEvent));
        Assert.Equal(templateParts.PART_Field.Text, editableString);

        string newText = "Bye bye!";
        templateParts.PART_Field.Text = newText;

        sut.OnFieldLostFocus(templateParts.PART_Field, new RoutedEventArgs(UIElement.LostFocusEvent));
        Assert.Equal(sut.FieldValue, newText);
        Assert.Equal(templateParts.PART_Field.Text, sut.FieldText);
    }
}
