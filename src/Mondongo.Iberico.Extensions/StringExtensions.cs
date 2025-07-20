// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="OscarNET-SOFTware">
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

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using Mondongo.Iberico.Extensions.Resources;

namespace Mondongo.Iberico.Extensions;

/// <summary>
/// Provides several extension methods to extend the strings functionality.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Defines the default ellipsis format.
    /// </summary>
    public const string DefaultEllipsisFormat = " (...)";

    /// <summary>
    /// Defines the maximum length of a secure string.
    /// </summary>
    public const int SecureStringMaxLength = ushort.MaxValue;

    /// <summary>
    /// Stores the folder separator.
    /// </summary>
    public static readonly char FolderSeparator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? '\\' : '/';

    /// <summary>
    /// Stores the invalid characters for file names.
    /// </summary>
    public static readonly ImmutableArray<char> InvalidCharsForFileNames = [.. Path.GetInvalidFileNameChars()];

    /// <summary>
    /// Stores the invalid characters for paths.
    /// </summary>
    public static readonly ImmutableArray<char> InvalidCharsForPaths = [.. Path.GetInvalidPathChars()];

    /// <summary>
    /// Formats the given message, if applicable.
    /// </summary>
    /// <param name="message">The message that should be formatted.</param>
    /// <param name="messageArguments">An object array containing zero or more objects to be used as arguments for formatting the message.</param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string FormatMessage(this string? message, params object[] messageArguments) =>
        FormatMessage(message, null, messageArguments);

    /// <summary>
    /// Formats the given message, if applicable.
    /// </summary>
    /// <param name="message">The message that should be formatted.</param>
    /// <param name="messageCulture">An object that supplies culture-specific formatting information.</param>
    /// <param name="messageArguments">An object array containing zero or more objects to be used as arguments for formatting the message.</param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string FormatMessage(this string? message, CultureInfo? messageCulture, params object[] messageArguments)
    {
        if (ShouldNotFormat(message, messageArguments))
        {
            return message ?? string.Empty;
        }

        CultureInfo culture = messageCulture ?? Thread.CurrentThread.CurrentCulture;

        try
        {
            return string.Format(culture, message!, messageArguments);
        }
        catch (FormatException formatException)
        {
            return string.Format(ExtensionsResources.InvalidFormatString,
                                 formatException.Message,
                                 message,
                                 messageArguments.Length);
        }
    }

    /// <summary>
    /// Determines whether a given full path is a valid full path.
    /// </summary>
    /// <param name="fullPath">The full path to validate.</param>
    /// <returns>
    ///   <c>true</c> if it's a valid path; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidFullPath(this string? fullPath)
    {
        if (!HasBasicPathProperties(fullPath))
        {
            return false;
        }

        try
        {
            if (!HasValidUncOrSeparator(fullPath!))
            {
                return false;
            }

            if (!HasValidFolderNames(fullPath!))
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Limits the given message, if applicable.
    /// </summary>
    /// <param name="message">The message that should be limited.</param>
    /// <param name="maxChars">Maximum allowed number of characters in the resulting limited message.</param>
    /// <param name="showEllipsis">If set to <c>true</c> (default value) shows ellipsis.</param>
    /// <returns>
    /// The limited message.
    /// </returns>
    public static string LimitMessage(this string? message, int maxChars, bool showEllipsis = true)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return string.Empty;
        }

        if (maxChars < 0)
        {
            return message;
        }

        string ellipsis = showEllipsis ? DefaultEllipsisFormat : string.Empty;
        int minLength = ellipsis.Length;
        int maxLength = Math.Max(minLength, maxChars);

        if (message.Length > maxLength)
        {
            return string.Concat(message.AsSpan(0, maxLength - minLength), ellipsis);
        }

        return message;
    }

    /// <summary>
    /// Removes the characters from a given string.
    /// </summary>
    /// <param name="originalString">The original string.</param>
    /// <param name="charactersToRemove">The characters to remove.</param>
    /// <param name="ignoreCase">It's set to <c>trueo</c> for ignoring case when comparing characters.</param>
    /// <param name="cultureInfo">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    /// The <paramref name="originalString" /> without the specified characters to remove.
    /// </returns>
    public static string? RemoveCharacters(this string? originalString,
                                           IEnumerable<char>? charactersToRemove,
                                           bool ignoreCase = true,
                                           CultureInfo? cultureInfo = null)
    {
        if (IsNullOrEmptyOrNoCharactersToRemove(originalString, charactersToRemove))
        {
            return originalString;
        }

        string charsToRemoveString = new([.. charactersToRemove!]);
        CompareInfo compareInfo = (cultureInfo ?? Thread.CurrentThread.CurrentCulture).CompareInfo;
        CompareOptions compareOptions = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;

        return BuildFilteredString(originalString!, charsToRemoveString, compareInfo, compareOptions);
    }

    /// <summary>
    /// Sanitizes a given folder name or file name.
    /// </summary>
    /// <param name="folderNameOrFileName">The folder name or file name.</param>
    /// <returns>
    /// The sanitized folder name or file name.
    /// </returns>
    public static string SanitizeFolderNameOrFileName([NotNull] this string? folderNameOrFileName)
    {
        if (string.IsNullOrWhiteSpace(folderNameOrFileName))
        {
            throw new ArgumentNullException(nameof(folderNameOrFileName));
        }

        StringBuilder sanitizedNameBuilder = new(folderNameOrFileName.Length);

        foreach (char character in folderNameOrFileName)
        {
            if (!InvalidCharsForPaths.Contains(character) && !InvalidCharsForFileNames.Contains(character))
            {
                sanitizedNameBuilder.Append(character);
            }
        }

        return sanitizedNameBuilder.ToString();
    }

    /// <summary>
    /// Converts the given string to digits only string.
    /// </summary>
    /// <param name="stringToConvert">The string to convert.</param>
    /// <returns>
    /// The specified string converted to digits only string.
    /// </returns>
    public static string ToDigits(this string? stringToConvert)
    {
        if (string.IsNullOrWhiteSpace(stringToConvert))
        {
            return stringToConvert ?? string.Empty;
        }

        return new string([.. stringToConvert.Where(char.IsDigit)]);
    }

    /// <summary>
    /// Converts the given <see cref="SecureString" /> to plain text string.
    /// </summary>
    /// <param name="secureStringToConvert">The <see cref="SecureString" /> to convert to plain text string.</param>
    /// <returns>
    /// The specified <see cref="SecureString" /> converted to plain text string.
    /// </returns>
    public static string ToPlainTextString(this SecureString? secureStringToConvert)
    {
        if (secureStringToConvert == null)
        {
            throw new ArgumentNullException(
                message: ExtensionsResources.SecureStringCanNotBeConvertedToStringBecauseIsNull,
                paramName: nameof(secureStringToConvert));
        }

        try
        {
            IntPtr unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureStringToConvert);
            return Marshal.PtrToStringUni(unmanagedString)!;
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(IntPtr.Zero);
        }
    }

    /// <summary>
    /// Converts the given string to <see cref="SecureString" />.
    /// </summary>
    /// <param name="stringToConvert">The string to convert to <see cref="SecureString" />.</param>
    /// <returns>
    /// The specified string converted to <see cref="SecureString" />.
    /// </returns>
    public static SecureString ToSecureString(this string? stringToConvert)
    {
        if (string.IsNullOrWhiteSpace(stringToConvert))
        {
            throw new ArgumentNullException(
                message: ExtensionsResources.StringCanNotBeConvertedToSecureStringBecauseIsNullOrWhiteSpace,
                paramName: nameof(stringToConvert));
        }

        if (stringToConvert.Length > SecureStringMaxLength)
        {
            throw new ArgumentException(
                message: ExtensionsResources.StringCanNotBeConvertedToSecureStringBecauseExceedsMaxLengthAllowed.FormatMessage(SecureStringMaxLength),
                paramName: nameof(stringToConvert));
        }

        unsafe
        {
            fixed (char* unsecureChars = stringToConvert)
            {
                SecureString secureString = new(unsecureChars, stringToConvert.Length);
                secureString.MakeReadOnly();
                return secureString;
            }
        }
    }

    /// <summary>
    /// Builds a filtered string.
    /// </summary>
    /// <param name="originalString">The original string.</param>
    /// <param name="charactersToRemove">The characters to remove.</param>
    /// <param name="compareInfo">A set of methods for comparing strings.</param>
    /// <param name="compareOptions">The string comparison options.</param>
    /// <returns>
    /// A <see cref="string" /> object containing the filtered string.
    /// </returns>
    private static string BuildFilteredString(string originalString,
                                              string charactersToRemove,
                                              CompareInfo compareInfo,
                                              CompareOptions compareOptions)
    {
        StringBuilder result = new(originalString.Length);

        foreach (char character in originalString)
        {
            if (compareInfo.IndexOf(charactersToRemove, character, compareOptions) == -1)
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Determines whether a given full path has basic path properties.
    /// </summary>
    /// <param name="fullPath">The full path to validate.</param>
    /// <returns>
    ///   <c>true</c> if it has basic path properties; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasBasicPathProperties(string? fullPath) =>
        !string.IsNullOrWhiteSpace(fullPath)
        && (fullPath.Length >= 3)
        && (fullPath.IndexOfAny([.. InvalidCharsForPaths]) == -1)
        && Path.IsPathRooted(fullPath);

    /// <summary>
    /// Determines whether a given full path has valid folder names.
    /// </summary>
    /// <param name="fullPath">The full path to validate.</param>
    /// <returns>
    ///   <c>true</c> if it has valid folder names; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasValidFolderNames(string fullPath)
    {
        string hypotheticalValidFullPath = Path.GetFullPath(fullPath);
        IEnumerable<string> hypotheticalValidFolders = hypotheticalValidFullPath
            .Split([FolderSeparator], StringSplitOptions.RemoveEmptyEntries)
            .Skip(1);

        foreach (string folderName in hypotheticalValidFolders)
        {
            if (folderName.Any(c => InvalidCharsForFileNames.Contains(c)))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether a given full path has valid UNC or separator.
    /// </summary>
    /// <param name="fullPath">The full path to validate.</param>
    /// <returns>
    ///   <c>true</c> if it has valid UNC or separator; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasValidUncOrSeparator(string fullPath)
    {
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        bool isUNCPath = isWindows && new Uri(fullPath).IsUnc;
        int indexOfDoubleSeparator = fullPath.LastIndexOf(new string(FolderSeparator, 2), StringComparison.Ordinal);

        if (isUNCPath)
        {
            return indexOfDoubleSeparator == 0;
        }

        return indexOfDoubleSeparator == -1;
    }

    /// <summary>
    /// Indicates whether the specified <paramref name="originalString" /> is <c>null</c> or an empty string (""),
    /// or whether the specified <paramref name="charactersToRemove" /> is <c>null</c>.
    /// </summary>
    /// <param name="originalString">The original string.</param>
    /// <param name="charactersToRemove">The characters to remove.</param>
    /// <returns>
    ///   <c>true</c>; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsNullOrEmptyOrNoCharactersToRemove(string? originalString, IEnumerable<char>? charactersToRemove) =>
        string.IsNullOrEmpty(originalString) || charactersToRemove == null;

    /// <summary>
    /// Checks if a given message can be formatted.
    /// </summary>
    /// <param name="message">The message that should be formatted.</param>
    /// <param name="messageArguments">An object array containing zero or more objects to be used as arguments
    /// for formatting the message.</param>
    /// <returns>
    ///   <c>true</c> it <paramref name="message" /> should not format; otherwise, <c>false</c>.
    /// </returns>
    private static bool ShouldNotFormat(string? message, object[] messageArguments) =>
        string.IsNullOrWhiteSpace(message) || messageArguments == null || messageArguments.Length == 0;
}
