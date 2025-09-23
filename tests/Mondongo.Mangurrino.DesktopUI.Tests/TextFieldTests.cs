// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFieldTests.cs" company="OscarNET-SOFTware">
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

public sealed class TextFieldTests
{
    [WpfFact]
    public void MaxAllowedLength_is_255()
    {
        var sut = new TextField();

        Assert.Equal(255, sut.MaxAllowedLength);
    }

    [WpfTheory]
    [InlineData("sample text", true, "sample text")]
    [InlineData("", true, "")]
    public void TryParseFieldValue_returns_expected(string input, bool expectedResult, string expectedValue)
    {
        var sut = new TextField();

        bool result = sut.TryParseFieldValue(input, out string? value);

        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedValue, value);
    }

    [WpfFact]
    public void FieldMaxLength_coerces_to_max_allowed_length()
    {
        var sut = new TextField
        {
            FieldMaxLength = 300
        };

        Assert.Equal(255, sut.FieldMaxLength);
    }
}
