// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBaseTests.Properties.cs" company="OscarNET-SOFTware">
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

public sealed partial class EditFieldBaseTests
{
    [WpfFact]
    public void FieldBackground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        SolidColorBrush brush = Brushes.Red;
        sut.FieldBackground = brush;

        Assert.Equal(brush, sut.FieldBackground);
    }

    [WpfFact]
    public void FieldBorderBrush_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        SolidColorBrush brush = Brushes.Blue;
        sut.FieldBorderBrush = brush;

        Assert.Equal(brush, sut.FieldBorderBrush);
    }

    [WpfFact]
    public void FieldBorderCornerRadius_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        var radius = new CornerRadius(5);
        sut.FieldBorderCornerRadius = radius;

        Assert.Equal(radius, sut.FieldBorderCornerRadius);
    }

    [WpfFact]
    public void FieldBorderThickness_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        var thickness = new Thickness(2, 3, 2, 3);
        sut.FieldBorderThickness = thickness;

        Assert.Equal(thickness, sut.FieldBorderThickness);
    }

    [WpfFact]
    public void FieldForeground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        SolidColorBrush brush = Brushes.Green;
        sut.FieldForeground = brush;

        Assert.Equal(brush, sut.FieldForeground);
    }

    [WpfFact]
    public void FieldHeight_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        double value = 100.5D;
        sut.FieldHeight = value;

        Assert.Equal(value, sut.FieldHeight);
    }

    [WpfFact]
    public void FieldTextAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase
        {
            FieldTextAlignment = TextAlignment.Center
        };

        Assert.Equal(TextAlignment.Center, sut.FieldTextAlignment);
    }

    [WpfFact]
    public void FieldWidth_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        double value = 200.25D;
        sut.FieldWidth = value;

        Assert.Equal(value, sut.FieldWidth);
    }

    [WpfFact]
    public void FieldHorizontalAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase
        {
            FieldHorizontalAlignment = HorizontalAlignment.Right
        };

        Assert.Equal(HorizontalAlignment.Right, sut.FieldHorizontalAlignment);
    }

    [WpfFact]
    public void LabelBackground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        SolidColorBrush brush = Brushes.Yellow;
        sut.LabelBackground = brush;

        Assert.Equal(brush, sut.LabelBackground);
    }

    [WpfFact]
    public void LabelForeground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        SolidColorBrush brush = Brushes.Black;
        sut.LabelForeground = brush;

        Assert.Equal(brush, sut.LabelForeground);
    }

    [WpfFact]
    public void LabelHeight_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        double value = 50D;
        sut.LabelHeight = value;

        Assert.Equal(value, sut.LabelHeight);
    }

    [WpfFact]
    public void LabelMargin_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        var margin = new Thickness(1, 2, 3, 4);
        sut.LabelMargin = margin;

        Assert.Equal(margin, sut.LabelMargin);
    }

    [WpfTheory]
    [InlineData(EditFieldLabelPosition.Left)]
    [InlineData(EditFieldLabelPosition.Top)]
    [InlineData(EditFieldLabelPosition.Right)]
    [InlineData(EditFieldLabelPosition.Bottom)]
    [InlineData(EditFieldLabelPosition.None)]
    public void LabelPosition_can_be_set_and_get_as_expected(EditFieldLabelPosition labelPosition)
    {
        var sut = new TestEditFieldBase
        {
            LabelPosition = labelPosition
        };

        Assert.Equal(labelPosition, sut.LabelPosition);
    }

    [WpfFact]
    public void LabelText_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        string text = "LabelTextExample";
        sut.LabelText = text;

        Assert.Equal(text, sut.LabelText);
    }

    [WpfFact]
    public void LabelTextAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase
        {
            LabelTextAlignment = TextAlignment.Right
        };

        Assert.Equal(TextAlignment.Right, sut.LabelTextAlignment);
    }

    [WpfFact]
    public void LabelWidth_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditFieldBase();

        double value = 75.75D;
        sut.LabelWidth = value;

        Assert.Equal(value, sut.LabelWidth);
    }
}
