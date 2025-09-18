// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBaseTests.Logic.cs" company="OscarNET-SOFTware">
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
        var sut = new TestEditFieldBase();

        _ = OnApplyTemplateSimulation(sut);

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
        var sut = new TestEditFieldBase();

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
        var sut = new TestEditFieldBase();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.InvokeGetGridPosition((EditFieldLabelPosition)999));
    }

    [WpfFact]
    public void UpdateLabelPosition_valid_parts_sets_visibility_and_grid_properties()
    {
        var sut = new TestEditFieldBase();

        (_, Border labelContent, _, _, _) = OnApplyTemplateSimulation(sut);

        sut.InvokeUpdateLabelPosition(EditFieldLabelPosition.Left);
        Assert.Equal(Visibility.Visible, labelContent.Visibility);

        sut.InvokeUpdateLabelPosition(EditFieldLabelPosition.None);
        Assert.Equal(Visibility.Collapsed, labelContent.Visibility);
    }

    [WpfFact]
    public void UpdateLabelMargin_valid_parts_sets_label_margin()
    {
        var sut = new TestEditFieldBase();

        var label = new TextBlock();
        sut._label = label;
        sut._labelContent = new Border();

        sut.InvokeUpdateLabelMargin(EditFieldLabelPosition.Left);

        Assert.Equal(new Thickness(0, 0, 7, 0), label.Margin);
    }

    [WpfFact]
    public void SetGridProperties_applies_grid_positions_correctly()
    {
        var sut = new TestEditFieldBase();

        (Grid grid, Border labelContent, _, Border fieldContent, _) =
            OnApplyTemplateSimulation(sut);

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
            Col1Width = new GridLength(1, GridUnitType.Star),
            Row0Height = new GridLength(2, GridUnitType.Pixel),
            Row1Height = GridLength.Auto
        };

        sut.InvokeSetGridProperties(gridPosition);

        Assert.Equal(gridPosition.Col0Width, grid.ColumnDefinitions[0].Width);
        Assert.Equal(gridPosition.Col1Width, grid.ColumnDefinitions[1].Width);
        Assert.Equal(gridPosition.Row0Height, grid.RowDefinitions[0].Height);
        Assert.Equal(gridPosition.Row1Height, grid.RowDefinitions[1].Height);

        Assert.Equal(gridPosition.LabelColumn, Grid.GetColumn(labelContent));
        Assert.Equal(gridPosition.LabelColumnSpan, Grid.GetColumnSpan(labelContent));
        Assert.Equal(gridPosition.LabelRow, Grid.GetRow(labelContent));
        Assert.Equal(gridPosition.LabelRowSpan, Grid.GetRowSpan(labelContent));

        Assert.Equal(gridPosition.FieldColumn, Grid.GetColumn(fieldContent));
        Assert.Equal(gridPosition.FieldColumnSpan, Grid.GetColumnSpan(fieldContent));
        Assert.Equal(gridPosition.FieldRow, Grid.GetRow(fieldContent));
        Assert.Equal(gridPosition.FieldRowSpan, Grid.GetRowSpan(fieldContent));
    }
}
