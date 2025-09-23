// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextField.cs" company="OscarNET-SOFTware">
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

/// <summary>
/// Provides functionality for text field editing.
/// </summary>
public class TextField : EditFieldBase<string?>
{
    /// <summary>
    /// Static constructor to perform one-time initialization for the <see cref="TextField" /> class.
    /// </summary>
    static TextField()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            forType: typeof(TextField),
            typeMetadata: new FrameworkPropertyMetadata(typeof(TextField)));

        LabelTextProperty.OverrideMetadata(
            forType: typeof(TextField),
            typeMetadata: new FrameworkPropertyMetadata(nameof(TextField)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextField" /> class.
    /// </summary>
    public TextField()
        : base()
    {
    }

    /// <summary>
    /// Gets the maximum allowed length for the field value.
    /// </summary>
    /// <value>
    /// The maximum length allowed for the field content.
    /// </value>
    protected internal override int MaxAllowedLength => 255;

    /// <summary>
    /// Converts the string representation of a given field to its type value equivalent.
    /// A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="fieldText">The string to parse.</param>
    /// <param name="fieldValue">When this method returns, contains the result of successfully parsing
    /// <paramref name="fieldText" /> or an undefined value on failure.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="fieldText" /> was successfully parsed; otherwise, <c>false</c>.
    /// </returns>
    protected internal override bool TryParseFieldValue(string fieldText, out string? fieldValue)
    {
        fieldValue = fieldText;
        return true;
    }
}
