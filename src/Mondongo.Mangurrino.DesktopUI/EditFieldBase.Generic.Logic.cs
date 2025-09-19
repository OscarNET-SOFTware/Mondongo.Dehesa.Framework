// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Generic.Logic.cs" company="OscarNET-SOFTware">
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

partial class EditFieldBase<TFieldValue>
{
    /// <summary>
    /// Is called when the control template is applied.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        UpdateFieldText(this, default);
    }

    /// <summary>
    /// Converts the value of a given field to its editable string equivalent.
    /// </summary>
    /// <param name="fieldValue">The value of the field.</param>
    /// <returns>
    /// The converted <paramref name="fieldValue" /> to its editable string equivalent.
    /// </returns>
    protected internal virtual string ConvertFieldValueToEditableString(TFieldValue? fieldValue) =>
        fieldValue?.ToString() ?? string.Empty;

    /// <summary>
    /// Occurs when the field gets logical focus.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    protected internal override void OnFieldGotFocus(object sender, RoutedEventArgs e)
    {
        TextBox field = (TextBox)sender;
        field.Text = ConvertFieldValueToEditableString(FieldValue);
        field.SelectAll();
    }

    /// <summary>
    /// Occurs when the field loses logical focus.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    protected internal override void OnFieldLostFocus(object sender, RoutedEventArgs e)
    {
        TextBox field = (TextBox)sender;
        if (TryParseFieldValue(field.Text, out TFieldValue? newValue))
        {
            FieldValue = newValue;
        }

        field.Text = FieldText;
    }

    /// <summary>
    /// Converts the string representation of a given field to its <typeparamref name="TFieldValue" />
    /// type value equivalent. A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="fieldText">The string to parse.</param>
    /// <param name="fieldValue">When this method returns, contains the result of successfully parsing
    /// <paramref name="fieldText" /> or an undefined value on failure.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="fieldText" /> was successfully parsed; otherwise, <c>false</c>.
    /// </returns>
    protected abstract bool TryParseFieldValue(string fieldText, out TFieldValue? fieldValue);

    /// <summary>
    /// Updates the text of the field.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase{TFieldValue}" /> instance reference.</param>
    /// <param name="fieldValue">Specifies the value of the field to update.</param>
    internal static void UpdateFieldText(EditFieldBase<TFieldValue> editField, TFieldValue? fieldValue)
    {
        if (!AreFieldPartsValid(editField))
        {
            return;
        }

        editField.FieldText = editField.ConvertFieldValueToEditableString(fieldValue);
        if (!editField._field!.IsFocused)
        {
            editField._field.Text = editField.FieldText;
        }
    }

    /// <summary>
    /// Called when the <see cref="FieldValue" /> dependency property changes.
    /// Updates the field text accordingly.
    /// </summary>
    /// <param name="d">The dependency object where the property changed.</param>
    /// <param name="e">Event data for the property change.</param>
    private static void OnFieldValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        EditFieldBase<TFieldValue> editField = (EditFieldBase<TFieldValue>)d;
        UpdateFieldText(editField, (TFieldValue?)e.NewValue);
    }
}
