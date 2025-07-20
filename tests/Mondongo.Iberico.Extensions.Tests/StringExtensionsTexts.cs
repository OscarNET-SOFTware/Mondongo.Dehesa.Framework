// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionsTexts.cs" company="OscarNET-SOFTware">
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

using System.Globalization;
using System.Security;

using Mondongo.Iberico.Extensions.Resources;

namespace Mondongo.Iberico.Extensions;

public sealed class StringExtensionsTexts
{
    private const string LoremIpsum = """
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam laoreet id libero vitae pellentesque.
        Duis quis malesuada tortor, dapibus feugiat felis. Nulla tristique tellus ac varius pharetra. Vestibulum ante
        ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Sed ullamcorper ex eget tincidunt
        fringilla. Curabitur quam elit, pretium quis nulla at, scelerisque posuere sapien. Etiam non fermentum quam.
        Nunc ultrices sapien leo, eu venenatis velit feugiat vulputate. Morbi id tincidunt velit, facilisis viverra
        magna. Nulla vel ante cursus, bibendum purus vel, vehicula elit. Duis augue arcu, vulputate vitae sodales nec,
        tincidunt quis magna. Integer vulputate at ligula quis rhoncus. Ut dapibus fermentum dolor, sit amet tempus
        lacus convallis ut. Morbi lobortis quam massa, eget dignissim arcu vulputate non. Proin quis condimentum lorem,
        non egestas risus. Donec ultrices, eros efficitur consectetur tristique, eros quam posuere arcu, sed luctus
        ipsum nisi vel sem.

        Nunc varius ultrices dui, in porttitor arcu pulvinar sit amet. Praesent consectetur commodo mauris, sed
        pharetra purus. Morbi at dignissim augue, id mollis ante. Vivamus quis ornare tortor, at aliquam justo. Nunc
        auctor porttitor ligula id ornare. Suspendisse hendrerit justo ac dolor congue eleifend. Aliquam lacus ipsum,
        malesuada sed erat ac, consectetur congue quam. Etiam vitae arcu vulputate, iaculis velit id, placerat libero.
        Fusce feugiat sit amet mi non ultrices.

        Nullam in felis accumsan, aliquet lacus in, lobortis ex. Aliquam in risus mi. Fusce sed nulla sollicitudin,
        vehicula libero ut, luctus elit. Nam quis magna lorem. Vivamus et luctus sem, at luctus nulla. Vestibulum nibh
        felis, imperdiet eget felis vitae, consectetur imperdiet leo. Integer fermentum turpis sit amet magna
        condimentum, vitae aliquam est fermentum. Nam sollicitudin erat id sollicitudin iaculis. Maecenas rutrum velit
        et tortor condimentum bibendum. Fusce sem eros, pharetra eu egestas quis, pulvinar at augue.
        """;

    public const string SimplePlainTextString = "OscarNET-SOFTware";
    public static readonly SecureString s_simpleSecureString = SimplePlainTextString.ToSecureString();

    public static readonly TheoryData<string, string, char[], bool, string> s_removeCharsData = new()
    {
        { "ÁÉÍÓÚáéíóúÑñ", "ÁÍÓÚáéíúñ", new char[] { 'É', 'ó', 'Ñ' }, false, "es-ES" },
        { "ÁÉÍÓÚáéíóúÑñ", "ÁÍÚáíú", new char[] { 'É', 'ó', 'Ñ' }, true, "es-ES" },
        { "ÂÊÔâêôÃãÕõÇç", "ÂÔâêôÃãÕç", new char[] { 'Ê', 'õ', 'Ç' }, false, "pt-PT" },
        { "ÂÊÔâêôÃãÕõÇç", "ÂÔâôÃã", new char[] { 'Ê', 'õ', 'Ç' }, true, "pt-PT" }
    };

    public static readonly TheoryData<string?, string, string?, DateTime?> s_formatMessageDateTimeData = new()
    {
        { "Fecha: {0:dd/MM/yyyy}", "Fecha: 01/05/2025", null, new DateTime(2025,5,1) },
        { "Fecha: {0:dd/MM/yyyy}", "Fecha: 01/05/2025", "es-ES", new DateTime(2025,5,1) },
        { "Hora: {0}", "Hora: {0}", null, null },
        { "Hora: {0}", "Hora: {0}", "es-ES", null }
    };

    public static readonly TheoryData<string?, string, string?, decimal> s_formatMessageDecimalData = new()
    {
        { "Importe: {0:#,##0.00}", "Importe: 2.025,25", "es-ES", 2025.25M },
        { "Amount: {0:#,##0.00}", "Amount: 2,025.25", "en-US", 2025.25M }
    };

    public static readonly TheoryData<string?, string, string?, int> s_formatMessageIntegerData = new()
    {
        { null, "", null, 1 },
        { null, "", "es-ES", 1 },
        { "   ", "   ", null, 1 },
        { "   ", "   ", "es-ES", 1 }
    };

    public static readonly TheoryData<string, string?, decimal> s_formatMessageWithInvalidFormatData = new()
    {
        { "Importe: {0:#,##0.00} | Fecha {1:dd/MM/yyyy}", null, 2025.25M },
        { "Importe: {0:#,##0.00} | Fecha {1:dd/MM/yyyy}", "es-ES", 2025.25M },
        { "Amount: {0:#,##0.00} | Date {1:MM/dd/yy}", null, 2025.25M },
        { "Amount: {0:#,##0.00} | Date {1:MM/dd/yy}", "en-US", 2025.25M }
    };

    [Theory]
    [MemberData(nameof(s_formatMessageDateTimeData))]
    [MemberData(nameof(s_formatMessageDecimalData))]
    [MemberData(nameof(s_formatMessageIntegerData))]
    public void FormatMessage_should_returns_expected_formatted_message(string? message,
                                                                        string expectedMessage,
                                                                        string? cultureName,
                                                                        params object[] messageArguments)
    {
        string sut = (cultureName != null)
            ? StringExtensions.FormatMessage(message, new CultureInfo(cultureName), messageArguments)
            : StringExtensions.FormatMessage(message, messageArguments);

        Assert.Equal(expectedMessage, sut);
    }

    [Theory]
    [MemberData(nameof(s_formatMessageWithInvalidFormatData))]
    public void FormatMessage_should_returns_expected_message_with_invalid_format(string message,
                                                                                  string? cultureName,
                                                                                  params object[] messageArguments)
    {
        string formatExceptionMessage = string.Empty;

        try
        {
            _ = string.Format(message, messageArguments);
        }
        catch (FormatException formatException)
        {
            formatExceptionMessage = formatException.Message;
        }

        string expectedMessage = string.Format(ExtensionsResources.InvalidFormatString,
                                               formatExceptionMessage,
                                               message,
                                               messageArguments.Length);

        string sut = (cultureName != null)
            ? StringExtensions.FormatMessage(message, new CultureInfo(cultureName), messageArguments)
            : StringExtensions.FormatMessage(message, messageArguments);

        Assert.Equal(expectedMessage, sut);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(@"Windows")]
    [InlineData(@"Windows\Offline Web Pages")]
    [InlineData(@"\Windows\Offline Web Pages")]
    [InlineData(@"C:Windows\Offline Web Pages")]
    [InlineData(@"C:\Windows\Offline Web Pages\\")]
    [InlineData(@"C:\Windows\Offline Web Pages\All*?\Books:\From Agile to DevOps at Microsoft Developer Division <January|2000>.html")]
    [InlineData(@"C:\Windows\Offline Web Pages\All\Books:\From Agile to DevOps at Microsoft Developer Division <January|2000>.html")]
    [InlineData(@"C:\Windows\Offline Web Pages\All\Books\From Agile to DevOps at Microsoft Developer Division <January|2000>.html")]
    [InlineData(@"\\SERVER\C:Windows\Offline Web Pages")]
    [InlineData(@"\\SERVER\C$\Windows\Offline Web Pages\\")]
    [InlineData(@"\\123.123.123.123\C:Windows\Offline Web Pages")]
    [InlineData(@"\\123.123.123.123\C$\Windows\Offline Web Pages\\")]
    [InlineData(@"/etc/init.d")]
    [InlineData(@"/etc/ssl/certs/")]
    public void IsValidFullPath_should_returns_false_when_full_path_is_not_valid(string? fullPath)
    {
        Assert.False(StringExtensions.IsValidFullPath(fullPath));
    }

    [Theory]
    [InlineData(@"C:\Windows\Offline Web Pages")]
    [InlineData(@"C:\Windows\Offline Web Pages\")]
    [InlineData(@"C:\Windows\Offline Web Pages\All\Books\From Agile to DevOps at Microsoft Developer Division January-2000.html")]
    [InlineData(@"\\SERVER\C$\Windows\Offline Web Pages")]
    [InlineData(@"\\123.123.123.123\C$\Windows\Offline Web Pages\")]
    public void IsValidFullPath_should_returns_true_when_full_path_is_valid(string fullPath)
    {
        Assert.True(StringExtensions.IsValidFullPath(fullPath));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void LimitMessage_should_returns_empty_string_when_message_is_null(string? message)
    {
        Assert.Empty(StringExtensions.LimitMessage(message, 15));
    }

    [Theory]
    [InlineData(LoremIpsum, 1000, false)]
    [InlineData(LoremIpsum, 2000, true)]
    public void LimitMessage_should_returns_expected_message_when_it_exceeds_max_chars(string message,
                                                                                       int maxChars,
                                                                                       bool showEllipsis)
    {
        string expectedMessage = showEllipsis
            ? LoremIpsum[..(maxChars - StringExtensions.DefaultEllipsisFormat.Length)] + StringExtensions.DefaultEllipsisFormat
            : LoremIpsum[..maxChars];

        Assert.Equal(expectedMessage, StringExtensions.LimitMessage(message, maxChars, showEllipsis));
    }

    [Fact]
    public void LimitMessage_should_returns_original_message_when_max_chars_are_less_than_zero()
    {
        Assert.Equal(LoremIpsum, StringExtensions.LimitMessage(LoremIpsum, -75));
    }

    [Fact]
    public void LimitMessage_should_returns_original_message_when_max_chars_do_not_exceed_message_length()
    {
        Assert.Equal(LoremIpsum, StringExtensions.LimitMessage(LoremIpsum, LoremIpsum.Length));
    }

    [Theory]
    [MemberData(nameof(s_removeCharsData))]
    public void RemoveCharacters_should_returns_expected_string_without_removed_chars(string originalString,
                                                                                      string expectedString,
                                                                                      char[] charactersToRemove,
                                                                                      bool ignoreCase,
                                                                                      string? cultureName)
    {
        CultureInfo? cultureInfo = (cultureName != null) ? new(cultureName) : null;

        Assert.Equal(expected: expectedString,
                     actual: StringExtensions.RemoveCharacters(originalString, charactersToRemove, ignoreCase, cultureInfo));
    }

    [Fact]
    public void RemoveCharacters_should_returns_null_when_original_string_is_null()
    {
        Assert.Null(StringExtensions.RemoveCharacters(null, "@#$".ToCharArray()));
    }

    [Theory]
    [InlineData(SimplePlainTextString, null)]
    [InlineData(SimplePlainTextString, new char[0])]
    public void RemoveCharacters_should_returns_original_string_when_characters_to_remove_are_missing(string originalString,
                                                                                                      char[]? charactersToRemove)
    {
        Assert.Equal(originalString, StringExtensions.RemoveCharacters(originalString, charactersToRemove));
    }

    [Theory]
    [InlineData("<>|:*?/", "")]
    [InlineData("España|Spain.txt*", "EspañaSpain.txt")]
    [InlineData("<España> | <Spain>:.txt?", "España  Spain.txt")]
    [InlineData("{España}_{Spain}.txt", "{España}_{Spain}.txt")]
    public void SanitizeFolderNameOrFileName_should_returns_sanitized_name(string folderNameOrFileName,
                                                                           string sanitizedName)
    {
        Assert.Equal(sanitizedName, StringExtensions.SanitizeFolderNameOrFileName(folderNameOrFileName));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SanitizeFolderNameOrFileName_should_throws_exception_when_name_is_missing(string? folderNameOrFileName)
    {
        Assert.Throws<ArgumentNullException>(() => _ = StringExtensions.SanitizeFolderNameOrFileName(folderNameOrFileName));
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("   ", "   ")]
    [InlineData("abc123", "123")]
    [InlineData("1a2b3c", "123")]
    [InlineData("NoDigits", "")]
    public void ToDigits_should_returns_expected_string(string? stringToConvert, string expectedString)
    {
        string sut = StringExtensions.ToDigits(stringToConvert);

        Assert.Equal(expectedString, sut);
    }

    [Fact]
    public void ToPlainTextString_should_returns_expected_plain_text_string()
    {
        Assert.Equal(SimplePlainTextString, s_simpleSecureString.ToPlainTextString());
    }

    [Fact]
    public void ToPlainTextString_should_throws_exception_when_secure_string_to_convert_is_missing()
    {
        SecureString? sut = null;

        string sutMessage = Assert.Throws<ArgumentNullException>(() => _ = sut.ToPlainTextString()).Message;
        Assert.StartsWith(ExtensionsResources.SecureStringCanNotBeConvertedToStringBecauseIsNull, sutMessage);
    }

    [Fact]
    public void ToSecureString_should_returns_expected_secure_string()
    {
        Assert.Equal(s_simpleSecureString.ToPlainTextString(), SimplePlainTextString.ToSecureString().ToPlainTextString());
    }

    [Fact]
    public void ToSecureString_should_throws_exception_when_string_to_convert_exceeds_max_length_allowed()
    {
        string sut = new('X', StringExtensions.SecureStringMaxLength + 1);

        string sutError = Assert.Throws<ArgumentException>(() => _ = sut.ToSecureString()).Message;
        Assert.StartsWith(
            ExtensionsResources.StringCanNotBeConvertedToSecureStringBecauseExceedsMaxLengthAllowed.FormatMessage(StringExtensions.SecureStringMaxLength),
            sutError);
    }

    [Fact]
    public void ToSecureString_should_throws_exception_when_string_to_convert_is_missing()
    {
        string? sut = null;

        string sutError = Assert.Throws<ArgumentNullException>(() => _ = sut.ToSecureString()).Message;
        Assert.StartsWith(
            string.Format(ExtensionsResources.StringCanNotBeConvertedToSecureStringBecauseIsNullOrWhiteSpace, StringExtensions.SecureStringMaxLength),
            sutError);
    }
}
