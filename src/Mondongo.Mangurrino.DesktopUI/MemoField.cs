// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoField.cs" company="OscarNET-SOFTware">
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

/// <summary>
/// Provides functionality for memo field editing.
/// </summary>
public class MemoField : EditFieldBase<string?>
{
    /// <summary>
    /// Defines the default height for the field.
    /// </summary>
    private const double DefaultFieldHeight = 90D;

    /// <summary>
    /// Defines the default width for the field.
    /// </summary>
    private const double DefaultFieldWidth = 500D;

    /// <summary>
    /// Static constructor to perform one-time initialization for the <see cref="MemoField" /> class.
    /// </summary>
    static MemoField()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            forType: typeof(MemoField),
            typeMetadata: new FrameworkPropertyMetadata(typeof(MemoField)));

        FieldHeightProperty.OverrideMetadata(
            forType: typeof(MemoField),
            typeMetadata: new FrameworkPropertyMetadata(DefaultFieldHeight));

        FieldWidthProperty.OverrideMetadata(
            forType: typeof(MemoField),
            typeMetadata: new FrameworkPropertyMetadata(DefaultFieldWidth));

        LabelTextProperty.OverrideMetadata(
            forType: typeof(MemoField),
            typeMetadata: new FrameworkPropertyMetadata(nameof(MemoField)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoField" /> class.
    /// </summary>
    public MemoField()
        : base()
    {
    }

    /// <summary>
    /// Is called when the control template is applied.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        EnableMultiLine(this);
    }

    /// <summary>
    /// Gets the maximum allowed length for the field value.
    /// </summary>
    /// <value>
    /// The maximum length allowed for the field content.
    /// </value>
    protected internal override int MaxAllowedLength => 4000;

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

    /// <summary>
    /// Allows the multi-line text in the field.
    /// </summary>
    /// <param name="memoField">The <see cref="MemoField" /> instance reference.</param>
    internal static void EnableMultiLine(MemoField memoField)
    {
        if (!AreFieldPartsValid(memoField))
        {
            return;
        }

        memoField._field!.AcceptsReturn = true;
        memoField._field.TextWrapping = TextWrapping.Wrap;
        memoField._field.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    }
}
