// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.cs" company="OscarNET-SOFTware">
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
using System.Windows;
using System.Windows.Controls;

namespace Mondongo.Mangurrino.DesktopUI;

/// <summary>
/// An abstract base class that provides functionality for field editing.
/// </summary>
public abstract partial class EditFieldBase : Control
{
    /// <summary>
    /// Defines the default width for the field.
    /// </summary>
    private const double DefaultFieldWidth = 250D;

    /// <summary>
    /// Defines the default margin values for the label according to its position.
    /// </summary>
    private static readonly ImmutableDictionary<EditFieldLabelPosition, Thickness> s_defaultLabelMargin =
        ImmutableDictionary.Create<EditFieldLabelPosition, Thickness>()
            .Add(EditFieldLabelPosition.Left, new Thickness(left: 0D, top: 0D, right: 7D, bottom: 0D))
            .Add(EditFieldLabelPosition.Top, new Thickness(left: 0D, top: 0D, right: 0D, bottom: 3D))
            .Add(EditFieldLabelPosition.Right, new Thickness(left: 7D, top: 0D, right: 0D, bottom: 0D))
            .Add(EditFieldLabelPosition.Bottom, new Thickness(left: 0D, top: 3D, right: 0D, bottom: 0D))
            .Add(EditFieldLabelPosition.None, new Thickness());

    /// <summary>
    /// Stores the <see cref="TextBox" /> instance reference that represents the field part.
    /// </summary>
    internal TextBox? _field;

    /// <summary>
    /// Stores the <see cref="Border" /> instance reference that represents the field content part.
    /// </summary>
    internal Border? _fieldContent;

    /// <summary>
    /// Stores the <see cref="TextBlock" /> instance reference that represents the label part.
    /// </summary>
    internal TextBlock? _label;

    /// <summary>
    /// Stores the <see cref="Border" /> instance reference that represents the label content part.
    /// </summary>
    internal Border? _labelContent;

    /// <summary>
    /// Stores the <see cref="Grid" /> instance reference that represents the layout root part.
    /// </summary>
    internal Grid? _layoutRoot;

    /// <summary>
    /// Static constructor to perform one-time initialization for the <see cref="EditFieldBase" /> class.
    /// </summary>
    static EditFieldBase()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            forType: typeof(EditFieldBase),
            typeMetadata: new FrameworkPropertyMetadata(typeof(EditFieldBase)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EditFieldBase" /> class.
    /// </summary>
    protected EditFieldBase()
        : base()
    {
        Unloaded += OnEditFieldUnloaded;
    }

    /// <summary>
    /// Checks whether the field-related template parts are successfully retrieved and not null.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance to validate.</param>
    /// <returns>
    ///   <c>True</c> if both <c>_fieldContent</c> and <c>_field</c> are not null; otherwise, <c>false</c>.
    /// </returns>
    protected static bool AreFieldPartsValid(EditFieldBase editField) =>
        editField._fieldContent is not null
        && editField._field is not null;

    /// <summary>
    /// Checks whether the label-related template parts are successfully retrieved and not null.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance to validate.</param>
    /// <returns>
    ///   <c>True</c> if both <c>_labelContent</c> and <c>_label</c> are not null; otherwise, <c>false</c>.
    /// </returns>
    protected static bool AreLabelPartsValid(EditFieldBase editField) =>
        editField._labelContent is not null
        && editField._label is not null;

    /// <summary>
    /// Checks whether all required template parts of the given <see cref="EditFieldBase" />
    /// instance are successfully retrieved and not null by aggregating validations of layout,
    /// label, and field parts.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance of the control to validate
    /// its template parts.</param>
    /// <returns>
    ///   <c>True</c> if all template parts are valid (non-null); otherwise, <c>false</c>.
    /// </returns>
    protected static bool AreTemplatePartsValid(EditFieldBase editField) =>
        IsLayoutRootValid(editField)
        && AreLabelPartsValid(editField)
        && AreFieldPartsValid(editField);

    /// <summary>
    /// Checks whether the layout root template part is successfully retrieved and not null.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance to validate.</param>
    /// <returns>
    ///   <c>True</c> if <c>_layoutRoot</c> is not null; otherwise, <c>false</c>.
    /// </returns>
    protected static bool IsLayoutRootValid(EditFieldBase editField) =>
        editField._layoutRoot is not null;
    /// <summary>
    /// Returns the named element in the visual tree of an instantiated <see cref="ControlTemplate" />.
    /// </summary>
    /// <param name="name">Name of the child to find.</param>
    /// <returns>
    /// The requested element. May be null if no element of the requested name exists.
    /// </returns>
    /// <remarks>
    /// This is a wrapper of the <see cref="FrameworkElement.GetTemplateChild(string)" /> method,
    /// for testing purposes.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    protected virtual object? GetTemplateChildWrapper(string name) => GetTemplateChild(name);

    /// <summary>
    /// Occurs when the field gets logical focus.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    [ExcludeFromCodeCoverage]
    protected internal virtual void OnFieldGotFocus(object sender, RoutedEventArgs e) { }

    /// <summary>
    /// Occurs when the field loses logical focus.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    [ExcludeFromCodeCoverage]
    protected internal virtual void OnFieldLostFocus(object sender, RoutedEventArgs e) { }

    /// <summary>
    /// Occurs when the element is removed from within an element tree of loaded elements.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    [ExcludeFromCodeCoverage]
    private static void OnEditFieldUnloaded(object sender, RoutedEventArgs e) =>
        ((EditFieldBase)sender).UnsubscribeEvents();
}
