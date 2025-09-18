// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Properties.cs" company="OscarNET-SOFTware">
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
using System.Windows.Media;

namespace Mondongo.Mangurrino.DesktopUI;

partial class EditFieldBase
{
    /// <summary>
    /// Gets or sets the <see cref="Brush" /> that describes the background color of the field.
    /// </summary>
    /// <value>
    /// The <see cref="Brush" /> that is used to fill the background color of the field.
    /// </value>
    public Brush FieldBackground
    {
        get => (Brush)GetValue(FieldBackgroundProperty);
        set => SetValue(FieldBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush" /> that draws the outer border color of the field.
    /// </summary>
    /// <value>
    /// The <see cref="Brush" /> that draws the outer border color of the field.
    /// </value>
    public Brush FieldBorderBrush
    {
        get => (Brush)GetValue(FieldBorderBrushProperty);
        set => SetValue(FieldBorderBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that represents the degree to which the corners
    /// of the field border are rounded.
    /// </summary>
    /// <value>
    /// The <see cref="CornerRadius" /> that describes the degree to which corners are rounded.
    /// </value>
    public CornerRadius FieldBorderCornerRadius
    {
        get => (CornerRadius)GetValue(FieldBorderCornerRadiusProperty);
        set => SetValue(FieldBorderCornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the relative <see cref="Thickness" /> of the field border.
    /// </summary>
    /// <value>
    /// The <see cref="Thickness" /> that describes the width of the boundaries of the field border.
    /// </value>
    public Thickness FieldBorderThickness
    {
        get => (Thickness)GetValue(FieldBorderThicknessProperty);
        set => SetValue(FieldBorderThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush" /> that describes the foreground color of the field.
    /// </summary>
    /// <value>
    /// The <see cref="Brush" /> that paints the foreground color of the field.
    /// </value>
    public Brush FieldForeground
    {
        get => (Brush)GetValue(FieldForegroundProperty);
        set => SetValue(FieldForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the height of the field.
    /// </summary>
    /// <value>
    /// The height of the field, in device-independent units (1/96th inch per unit).
    /// </value>
    public double FieldHeight
    {
        get => (double)GetValue(FieldHeightProperty);
        set => SetValue(FieldHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the text alignment of the field.
    /// </summary>
    /// <value>
    /// One of the <see cref="TextAlignment" /> values that specifies the text alignment of the field.
    /// </value>
    public TextAlignment FieldTextAlignment
    {
        get => (TextAlignment)GetValue(FieldTextAlignmentProperty);
        set => SetValue(FieldTextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the field.
    /// </summary>
    /// <value>
    /// The width of the field, in device-independent units (1/96th inch per unit).
    /// </value>
    public double FieldWidth
    {
        get => (double)GetValue(FieldWidthProperty);
        set => SetValue(FieldWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of the field.
    /// </summary>
    /// <value>
    /// One of the <see cref="HorizontalAlignment" /> values that specifies
    /// the horizontal alignment of the field.
    /// </value>
    public HorizontalAlignment FieldHorizontalAlignment
    {
        get => (HorizontalAlignment)GetValue(FieldHorizontalAlignmentProperty);
        set => SetValue(FieldHorizontalAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush" /> that describes the background color of the label.
    /// </summary>
    /// <value>
    /// The <see cref="Brush" /> that is used to fill the background color of the label.
    /// </value>
    public Brush LabelBackground
    {
        get => (Brush)GetValue(LabelBackgroundProperty);
        set => SetValue(LabelBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush" /> that describes the foreground color of the label.
    /// </summary>
    /// <value>
    /// The <see cref="Brush" /> that paints the foreground color of the label.
    /// </value>
    public Brush LabelForeground
    {
        get => (Brush)GetValue(LabelForegroundProperty);
        set => SetValue(LabelForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the height of the label.
    /// </summary>
    /// <value>
    /// The height of the label, in device-independent units (1/96th inch per unit).
    /// </value>
    public double LabelHeight
    {
        get => (double)GetValue(LabelHeightProperty);
        set => SetValue(LabelHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the outer margin of the label.
    /// </summary>
    /// <value>
    /// Provides margin values for the label.
    /// </value>
    public Thickness LabelMargin
    {
        get => (Thickness)GetValue(LabelMarginProperty);
        set => SetValue(LabelMarginProperty, value);
    }

    /// <summary>
    /// Gets or sets the position of the label relative to the field.
    /// </summary>
    /// <value>
    /// One of the <see cref="EditFieldLabelPosition" /> values that specifies the position of the label.
    /// </value>
    public EditFieldLabelPosition LabelPosition
    {
        get => (EditFieldLabelPosition)GetValue(LabelPositionProperty);
        set => SetValue(LabelPositionProperty, value);
    }

    /// <summary>
    /// Gets or sets the text of the label.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> containing the text of the label.
    /// </value>
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text alignment of the label.
    /// </summary>
    /// <value>
    /// One of the <see cref="TextAlignment" /> values that specifies the text alignment of the label.
    /// </value>
    public TextAlignment LabelTextAlignment
    {
        get => (TextAlignment)GetValue(LabelTextAlignmentProperty);
        set => SetValue(LabelTextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the label.
    /// </summary>
    /// <value>
    /// The width of the label, in device-independent units (1/96th inch per unit).
    /// </value>
    public double LabelWidth
    {
        get => (double)GetValue(LabelWidthProperty);
        set => SetValue(LabelWidthProperty, value);
    }
}
