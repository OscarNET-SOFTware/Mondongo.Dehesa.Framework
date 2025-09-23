// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Logic.cs" company="OscarNET-SOFTware">
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

partial class EditFieldBase
{
    /// <summary>
    /// Is called when the control template is applied.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        UnsubscribeEvents();

        _layoutRoot = GetTemplateChildWrapper("PART_LayoutRoot") as Grid;
        _labelContent = GetTemplateChildWrapper("PART_LabelContent") as Border;
        _label = GetTemplateChildWrapper("PART_Label") as TextBlock;
        _fieldContent = GetTemplateChildWrapper("PART_FieldContent") as Border;
        _field = GetTemplateChildWrapper("PART_Field") as TextBox;

        SubscribeEvents();

        UpdateLabelPosition(this, LabelPosition);
        UpdateLabelMargin(this, LabelPosition);
    }

    /// <summary>
    /// Subscribe the necessary event handlers.
    /// </summary>
    protected internal virtual void SubscribeEvents()
    {
        if (_field != null)
        {
            _field.GotFocus += OnFieldGotFocus;
            _field.LostFocus += OnFieldLostFocus;
        }
    }

    /// <summary>
    /// Unsubscribe event handlers to avoid memory leaks.
    /// </summary>
    protected internal virtual void UnsubscribeEvents()
    {
        if (_field != null)
        {
            _field.GotFocus -= OnFieldGotFocus;
            _field.LostFocus -= OnFieldLostFocus;
        }
    }

    /// <summary>
    /// Coerces a field length value to be within the allowed range defined by
    /// <see cref="MinAllowedLength"/> and <see cref="MaxAllowedLength"/>.
    /// </summary>
    /// <param name="d">The dependency object on which the property is set.</param>
    /// <param name="baseValue">The proposed value to be coerced.</param>
    /// <returns>
    /// The coerced value, guaranteed to fall between
    /// <see cref="MinAllowedLength"/> and <see cref="MaxAllowedLength"/> inclusive.
    /// </returns>
    internal static object CoerceFieldLength(DependencyObject d, object baseValue)
    {
        var control = (EditFieldBase)d;
        int value = (int)baseValue;

        int minAllowed = control.MinAllowedLength;
        int maxAllowed = control.MaxAllowedLength;

        if (value < minAllowed) return minAllowed;
        if (value > maxAllowed) return maxAllowed;

        return value;
    }

    /// <summary>
    /// Returns the grid layout configuration corresponding to the specified label position.
    /// </summary>
    /// <param name="labelPosition">The desired <see cref="EditFieldLabelPosition"/> indicating
    /// where the label should be positioned relative to the field.</param>
    /// <returns>
    /// An <see cref="EditFieldGridPosition"/> struct containing grid row/column indices, spans,
    /// and sizing values tailored to the given label position.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="labelPosition" /> is outside the defined range of
    /// <see cref="EditFieldLabelPosition"/> enum values.
    /// </exception>
    internal static EditFieldGridPosition GetGridPosition(EditFieldLabelPosition labelPosition) =>
        labelPosition switch
        {
            EditFieldLabelPosition.Left or EditFieldLabelPosition.None => new()
            {
                LabelColumn = 0,
                LabelColumnSpan = 1,
                LabelRow = 0,
                LabelRowSpan = 1,
                FieldColumn = 1,
                FieldColumnSpan = 1,
                FieldRow = 0,
                FieldRowSpan = 1,
                Col0Width = GridLength.Auto,
                Col1Width = new GridLength(1, GridUnitType.Star),
                Row0Height = new GridLength(1, GridUnitType.Star),
                Row1Height = GridLength.Auto
            },
            EditFieldLabelPosition.Top => new()
            {
                LabelColumn = 0,
                LabelColumnSpan = 2,
                LabelRow = 0,
                LabelRowSpan = 1,
                FieldColumn = 0,
                FieldColumnSpan = 2,
                FieldRow = 1,
                FieldRowSpan = 1,
                Col0Width = new GridLength(1, GridUnitType.Star),
                Col1Width = GridLength.Auto,
                Row0Height = GridLength.Auto,
                Row1Height = GridLength.Auto
            },
            EditFieldLabelPosition.Right => new()
            {
                LabelColumn = 1,
                LabelColumnSpan = 1,
                LabelRow = 0,
                LabelRowSpan = 1,
                FieldColumn = 0,
                FieldColumnSpan = 1,
                FieldRow = 0,
                FieldRowSpan = 1,
                Col0Width = new GridLength(1, GridUnitType.Star),
                Col1Width = GridLength.Auto,
                Row0Height = new GridLength(1, GridUnitType.Star),
                Row1Height = GridLength.Auto
            },
            EditFieldLabelPosition.Bottom => new()
            {
                LabelColumn = 0,
                LabelColumnSpan = 2,
                LabelRow = 1,
                LabelRowSpan = 1,
                FieldColumn = 0,
                FieldColumnSpan = 2,
                FieldRow = 0,
                FieldRowSpan = 1,
                Col0Width = new GridLength(1, GridUnitType.Star),
                Col1Width = GridLength.Auto,
                Row0Height = GridLength.Auto,
                Row1Height = GridLength.Auto
            },
            _ => throw new ArgumentOutOfRangeException(nameof(labelPosition), labelPosition, "Invalid label position!")
        };

    /// <summary>
    /// Applies the specified grid layout properties to the <see cref="EditFieldBase" /> control's
    /// template parts.
    /// </summary>
    /// <param name="editField">The instance of <see cref="EditFieldBase" /> whose grid layout
    /// will be updated.</param>
    /// <param name="gridPosition">The <see cref="EditFieldGridPosition"/> struct containing all
    /// grid positioning and sizing values to apply.</param>
    /// <remarks>
    /// This method sets the widths and heights of the first two column and row definitions of the
    /// control's layout grid.  It also sets the column, column span, row, and row span of the label
    /// and field content borders within the grid.
    /// </remarks>
    internal static void SetGridProperties(EditFieldBase editField, EditFieldGridPosition gridPosition)
    {
        Grid grid = editField._layoutRoot!;
        Border labelContent = editField._labelContent!;
        Border fieldContent = editField._fieldContent!;

        grid.ColumnDefinitions[0].Width = gridPosition.Col0Width;
        grid.ColumnDefinitions[1].Width = gridPosition.Col1Width;
        grid.RowDefinitions[0].Height = gridPosition.Row0Height;
        grid.RowDefinitions[1].Height = gridPosition.Row1Height;

        Grid.SetColumn(labelContent, gridPosition.LabelColumn);
        Grid.SetColumnSpan(labelContent, gridPosition.LabelColumnSpan);
        Grid.SetRow(labelContent, gridPosition.LabelRow);
        Grid.SetRowSpan(labelContent, gridPosition.LabelRowSpan);

        Grid.SetColumn(fieldContent, gridPosition.FieldColumn);
        Grid.SetColumnSpan(fieldContent, gridPosition.FieldColumnSpan);
        Grid.SetRow(fieldContent, gridPosition.FieldRow);
        Grid.SetRowSpan(fieldContent, gridPosition.FieldRowSpan);
    }

    /// <summary>
    /// Updates the position of the label.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance reference.</param>
    /// <param name="labelPosition">The <see cref="EditFieldLabelPosition" /> value that specifies
    /// the position of the label.</param>
    internal static void UpdateLabelPosition(EditFieldBase editField, EditFieldLabelPosition labelPosition)
    {
        if (!AreTemplatePartsValid(editField))
        {
            return;
        }

        editField._labelContent!.Visibility = labelPosition == EditFieldLabelPosition.None
            ? Visibility.Collapsed
            : Visibility.Visible;

        SetGridProperties(editField, GetGridPosition(labelPosition));

        string labelPositionName = Enum.GetName(labelPosition)!;
        editField._layoutRoot!.ColumnDefinitions[0].SharedSizeGroup = $"{labelPositionName}ColumnA";
        editField._layoutRoot.ColumnDefinitions[1].SharedSizeGroup = $"{labelPositionName}ColumnB";
        editField._layoutRoot.RowDefinitions[0].SharedSizeGroup = $"{labelPositionName}RowA";
        editField._layoutRoot.RowDefinitions[1].SharedSizeGroup = $"{labelPositionName}RowB";
    }

    /// <summary>
    /// Updates the margin of the label.
    /// </summary>
    /// <param name="editField">The <see cref="EditFieldBase" /> instance reference.</param>
    /// <param name="labelPosition">The <see cref="EditFieldLabelPosition" /> value that specifies
    /// the position of the label.</param>
    internal static void UpdateLabelMargin(EditFieldBase editField, EditFieldLabelPosition labelPosition)
    {
        if (!AreLabelPartsValid(editField))
        {
            return;
        }

        editField._label!.Margin = editField.ReadLocalValue(LabelMarginProperty) == DependencyProperty.UnsetValue
            ? s_defaultLabelMargin[labelPosition]
            : editField.LabelMargin;
    }

    /// <summary>
    /// Called when the <see cref="LabelMargin" /> dependency property changes.
    /// Updates the label position accordingly.
    /// </summary>
    /// <param name="d">The dependency object where the property changed.</param>
    /// <param name="e">Event data for the property change.</param>
    private static void OnLabelMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        EditFieldBase editField = (EditFieldBase)d;
        UpdateLabelMargin(editField, editField.LabelPosition);
    }

    /// <summary>
    /// Called when the <see cref="LabelPosition" /> dependency property changes.
    /// Updates the label position accordingly.
    /// </summary>
    /// <param name="d">The dependency object where the property changed.</param>
    /// <param name="e">Event data for the property change.</param>
    private static void OnLabelPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        EditFieldBase editField = (EditFieldBase)d;
        EditFieldLabelPosition labelPosition = (EditFieldLabelPosition)e.NewValue;
        UpdateLabelPosition(editField, labelPosition);
        UpdateLabelMargin(editField, labelPosition);
    }
}
