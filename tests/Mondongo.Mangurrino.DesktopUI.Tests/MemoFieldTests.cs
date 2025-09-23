// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoFieldTests.cs" company="OscarNET-SOFTware">
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
using System.Windows.Controls;

namespace Mondongo.Mangurrino.DesktopUI;

public sealed class MemoFieldTests
{
    [WpfFact]
    public void MaxAllowedLength_is_4000()
    {
        var sut = new MemoField();

        Assert.Equal(4000, sut.MaxAllowedLength);
    }

    [WpfTheory]
    [InlineData("some memo", true, "some memo")]
    [InlineData("", true, "")]
    public void TryParseFieldValue_returns_expected(string input, bool expectedResult, string expectedValue)
    {
        var sut = new MemoField();

        bool result = sut.TryParseFieldValue(input, out string? value);

        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedValue, value);
    }

    [WpfFact]
    public void FieldMaxLength_coerces_to_max_allowed_length()
    {
        var sut = new MemoField
        {
            FieldMaxLength = 10000
        };

        Assert.Equal(4000, sut.FieldMaxLength);
    }

    [WpfFact]
    public void EnableMultiLine_works_as_expected()
    {
        var sut = new TestEditField();

        TestEditFieldTemplateParts templateParts = sut.OnApplyTemplateSimulation();
        sut.SetupTemplateParts(templateParts);

        MemoField.EnableMultiLine(sut as MemoField ?? throw new InvalidCastException());

        Assert.True(templateParts.PART_Field.AcceptsReturn);
        Assert.Equal(TextWrapping.Wrap, templateParts.PART_Field.TextWrapping);
        Assert.Equal(ScrollBarVisibility.Auto, templateParts.PART_Field.VerticalScrollBarVisibility);
    }
}
