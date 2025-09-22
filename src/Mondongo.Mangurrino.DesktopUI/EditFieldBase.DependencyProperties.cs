// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.DependencyProperties.cs" company="OscarNET-SOFTware">
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
    /// Identifies the <see cref="FieldBackground" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldBackgroundProperty =
        DependencyProperty.Register(
            name: nameof(FieldBackground),
            propertyType: typeof(Brush),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(Brushes.GhostWhite));

    /// <summary>
    /// Identifies the <see cref="FieldBorderBrush" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldBorderBrushProperty =
        DependencyProperty.Register(
            name: nameof(FieldBorderBrush),
            propertyType: typeof(Brush),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(Brushes.RoyalBlue));

    /// <summary>
    /// Identifies the <see cref="FieldBorderCornerRadius" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldBorderCornerRadiusProperty =
        DependencyProperty.Register(
            name: nameof(FieldBorderCornerRadius),
            propertyType: typeof(CornerRadius),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(new CornerRadius(3D)));

    /// <summary>
    /// Identifies the <see cref="FieldBorderThickness" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldBorderThicknessProperty =
        DependencyProperty.Register(
            name: nameof(FieldBorderThickness),
            propertyType: typeof(Thickness),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(new Thickness(1D)));

    /// <summary>
    /// Identifies the <see cref="FieldForeground" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldForegroundProperty =
        DependencyProperty.Register(
            name: nameof(FieldForeground),
            propertyType: typeof(Brush),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(Brushes.RoyalBlue));

    /// <summary>
    /// Identifies the <see cref="FieldHeight" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldHeightProperty =
        DependencyProperty.Register(
            name: nameof(FieldHeight),
            propertyType: typeof(double),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(double.NaN));

    /// <summary>
    /// Identifies the <see cref="FieldHorizontalAlignment" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldHorizontalAlignmentProperty =
        DependencyProperty.Register(
            name: nameof(FieldHorizontalAlignment),
            propertyType: typeof(HorizontalAlignment),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(HorizontalAlignment.Left));

    /// <summary>
    /// Identifies the <see cref="FieldTextAlignment" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldTextAlignmentProperty =
        DependencyProperty.Register(
            name: nameof(FieldTextAlignment),
            propertyType: typeof(TextAlignment),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(TextAlignment.Left));

    /// <summary>
    /// Identifies the <see cref="FieldText" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldTextProperty =
        DependencyProperty.Register(
            name: nameof(FieldText),
            propertyType: typeof(string),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new FrameworkPropertyMetadata(
                defaultValue: string.Empty,
                flags: FrameworkPropertyMetadataOptions.NotDataBindable));

    /// <summary>
    /// Identifies the <see cref="FieldWidth" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty FieldWidthProperty =
        DependencyProperty.Register(
            name: nameof(FieldWidth),
            propertyType: typeof(double),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(DefaultFieldWidth));

    /// <summary>
    /// Identifies the <see cref="LabelBackground" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelBackgroundProperty =
        DependencyProperty.Register(
            name: nameof(LabelBackground),
            propertyType: typeof(Brush),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(Brushes.Transparent));

    /// <summary>
    /// Identifies the <see cref="LabelForeground" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelForegroundProperty =
        DependencyProperty.Register(
            name: nameof(LabelForeground),
            propertyType: typeof(Brush),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(Brushes.Black));

    /// <summary>
    /// Identifies the <see cref="LabelHeight" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelHeightProperty =
        DependencyProperty.Register(
            name: nameof(LabelHeight),
            propertyType: typeof(double),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(double.NaN));

    /// <summary>
    /// Identifies the <see cref="LabelMargin" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelMarginProperty =
        DependencyProperty.Register(
            name: nameof(LabelMargin),
            propertyType: typeof(Thickness),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(
                defaultValue: s_defaultLabelMargin[EditFieldLabelPosition.Left],
                propertyChangedCallback: OnLabelMarginChanged));

    /// <summary>
    /// Identifies the <see cref="LabelPosition" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelPositionProperty =
        DependencyProperty.Register(
            name: nameof(LabelPosition),
            propertyType: typeof(EditFieldLabelPosition),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(
                defaultValue: EditFieldLabelPosition.Left,
                propertyChangedCallback: OnLabelPositionChanged));

    /// <summary>
    /// Identifies the <see cref="LabelTextAlignment" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelTextAlignmentProperty =
        DependencyProperty.Register(
            name: nameof(LabelTextAlignment),
            propertyType: typeof(TextAlignment),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(TextAlignment.Left));

    /// <summary>
    /// Identifies the <see cref="LabelText" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            name: nameof(LabelText),
            propertyType: typeof(string),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(string.Empty));

    /// <summary>
    /// Identifies the <see cref="LabelWidth" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelWidthProperty =
        DependencyProperty.Register(
            name: nameof(LabelWidth),
            propertyType: typeof(double),
            ownerType: typeof(EditFieldBase),
            typeMetadata: new PropertyMetadata(double.NaN));

}
