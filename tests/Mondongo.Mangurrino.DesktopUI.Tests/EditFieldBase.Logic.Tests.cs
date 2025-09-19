// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Logic.Tests.cs" company="OscarNET-SOFTware">
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

partial class EditFieldBaseTests
{
    [WpfFact]
    public void OnApplyTemplate_valid_template_parts_calls_update_methods()
    {
        var sut = new TestEditField();

        _ = sut.OnApplyTemplateSimulation();

        Assert.NotNull(sut._layoutRoot);
        Assert.NotNull(sut._labelContent);
        Assert.NotNull(sut._label);
        Assert.NotNull(sut._fieldContent);
        Assert.NotNull(sut._field);
    }

    [WpfTheory]
    [InlineData(EditFieldLabelPosition.Left)]
    [InlineData(EditFieldLabelPosition.Top)]
    [InlineData(EditFieldLabelPosition.Right)]
    [InlineData(EditFieldLabelPosition.Bottom)]
    [InlineData(EditFieldLabelPosition.None)]
    public void GetGridPosition_valid_position_returns_expected_values(EditFieldLabelPosition labelPosition)
    {
        var sut = new TestEditField();

        EditFieldGridPosition result = sut.InvokeGetGridPosition(labelPosition);

        Assert.InRange(result.LabelColumn, 0, 1);
        Assert.InRange(result.LabelColumnSpan, 1, 2);
        Assert.InRange(result.LabelRow, 0, 1);
        Assert.InRange(result.LabelRowSpan, 1, 1);
        Assert.InRange(result.FieldColumn, 0, 1);
        Assert.InRange(result.FieldColumnSpan, 1, 2);
        Assert.InRange(result.FieldRow, 0, 1);
        Assert.InRange(result.FieldRowSpan, 1, 1);
        Assert.True(result.Col0Width.Value >= 0D);
        Assert.True(result.Col1Width.Value >= 0D);
        Assert.True(result.Row0Height.Value >= 0D);
        Assert.True(result.Row1Height.Value >= 0D);
    }

    [WpfFact]
    public void GetGridPosition_invalid_position_throws_exception()
    {
        var sut = new TestEditField();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.InvokeGetGridPosition((EditFieldLabelPosition)999));
    }

    [WpfFact]
    public void UpdateLabelPosition_valid_parts_sets_visibility_and_grid_properties()
    {
        var sut = new TestEditField();

        TestEditFieldTemplateParts templateParts = sut.OnApplyTemplateSimulation();

        sut.InvokeUpdateLabelPosition(EditFieldLabelPosition.Left);
        Assert.Equal(Visibility.Visible, templateParts.PART_LabelContent.Visibility);

        sut.InvokeUpdateLabelPosition(EditFieldLabelPosition.None);
        Assert.Equal(Visibility.Collapsed, templateParts.PART_LabelContent.Visibility);
    }

    [WpfFact]
    public void UpdateLabelMargin_valid_parts_sets_label_margin()
    {
        var sut = new TestEditField();

        var label = new TextBlock();
        sut._label = label;
        sut._labelContent = new Border();

        sut.InvokeUpdateLabelMargin(EditFieldLabelPosition.Left);
        Assert.Equal(new Thickness(left: 0D, top: 0D, right: 7D, bottom: 0D), label.Margin);

        var newLabelMargin = new Thickness(left: 7D, top: 0D, right: 0D, bottom: 0D);
        sut.LabelMargin = newLabelMargin;
        sut.InvokeUpdateLabelMargin(EditFieldLabelPosition.Right);
        Assert.Equal(newLabelMargin, label.Margin);
    }

    [WpfFact]
    public void SetGridProperties_applies_grid_positions_correctly()
    {
        var sut = new TestEditField();

        TestEditFieldTemplateParts templateParts = sut.OnApplyTemplateSimulation();

        var gridPosition = new EditFieldGridPosition
        {
            LabelColumn = 1,
            LabelColumnSpan = 1,
            LabelRow = 1,
            LabelRowSpan = 1,
            FieldColumn = 0,
            FieldColumnSpan = 2,
            FieldRow = 0,
            FieldRowSpan = 2,
            Col0Width = GridLength.Auto,
            Col1Width = new GridLength(1D, GridUnitType.Star),
            Row0Height = new GridLength(2D, GridUnitType.Pixel),
            Row1Height = GridLength.Auto
        };

        sut.InvokeSetGridProperties(gridPosition);

        Assert.Equal(gridPosition.Col0Width, templateParts.PART_LayoutRoot.ColumnDefinitions[0].Width);
        Assert.Equal(gridPosition.Col1Width, templateParts.PART_LayoutRoot.ColumnDefinitions[1].Width);
        Assert.Equal(gridPosition.Row0Height, templateParts.PART_LayoutRoot.RowDefinitions[0].Height);
        Assert.Equal(gridPosition.Row1Height, templateParts.PART_LayoutRoot.RowDefinitions[1].Height);

        Assert.Equal(gridPosition.LabelColumn, Grid.GetColumn(templateParts.PART_LabelContent));
        Assert.Equal(gridPosition.LabelColumnSpan, Grid.GetColumnSpan(templateParts.PART_LabelContent));
        Assert.Equal(gridPosition.LabelRow, Grid.GetRow(templateParts.PART_LabelContent));
        Assert.Equal(gridPosition.LabelRowSpan, Grid.GetRowSpan(templateParts.PART_LabelContent));

        Assert.Equal(gridPosition.FieldColumn, Grid.GetColumn(templateParts.PART_FieldContent));
        Assert.Equal(gridPosition.FieldColumnSpan, Grid.GetColumnSpan(templateParts.PART_FieldContent));
        Assert.Equal(gridPosition.FieldRow, Grid.GetRow(templateParts.PART_FieldContent));
        Assert.Equal(gridPosition.FieldRowSpan, Grid.GetRowSpan(templateParts.PART_FieldContent));
    }

    [WpfFact]
    public void SubscribeEvents_and_UnsubscribeEvents_works_as_expected()
    {
        var sut = new TestEditField();
        var field = new TextBox();

        sut.SetupTemplateParts(
            new TestEditFieldTemplateParts
            {
                PART_LayoutRoot = new Grid(),
                PART_LabelContent = new Border(),
                PART_Label = new TextBlock(),
                PART_FieldContent = new Border(),
                PART_Field = field
            });

        bool gotFocusHandled = false;
        bool lostFocusHandled = false;

        void gotFocusHandler(object s, RoutedEventArgs e) => gotFocusHandled = true;
        void lostFocusHandler(object s, RoutedEventArgs e) => lostFocusHandled = true;

        sut.SubscribeEvents();
        field.GotFocus += gotFocusHandler;
        field.LostFocus += lostFocusHandler;

        gotFocusHandled = false;
        field.RaiseEvent(new RoutedEventArgs(UIElement.GotFocusEvent));
        Assert.True(gotFocusHandled);

        lostFocusHandled = false;
        field.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
        Assert.True(lostFocusHandled);

        sut.UnsubscribeEvents();
        field.GotFocus -= gotFocusHandler;
        field.LostFocus -= lostFocusHandler;

        gotFocusHandled = false;
        field.RaiseEvent(new RoutedEventArgs(UIElement.GotFocusEvent));
        Assert.False(gotFocusHandled);

        lostFocusHandled = false;
        field.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
        Assert.False(lostFocusHandled);
    }
}
