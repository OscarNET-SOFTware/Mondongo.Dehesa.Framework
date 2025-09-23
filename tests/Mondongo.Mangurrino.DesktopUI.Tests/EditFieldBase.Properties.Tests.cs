// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFieldBase.Properties.Tests.cs" company="OscarNET-SOFTware">
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
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.Red;
        sut.FieldBackground = brush;

        Assert.Equal(brush, sut.FieldBackground);
    }

    [WpfFact]
    public void FieldBackgroundWhenGotFocus_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.Green;
        sut.FieldBackgroundWhenGotFocus = brush;

        Assert.Equal(brush, sut.FieldBackgroundWhenGotFocus);
    }

    [WpfFact]
    public void FieldBackgroundWhenIsDisabled_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.Blue;
        sut.FieldBackgroundWhenIsDisabled = brush;

        Assert.Equal(brush, sut.FieldBackgroundWhenIsDisabled);
    }

    [WpfFact]
    public void FieldBorderBrush_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.DarkRed;
        sut.FieldBorderBrush = brush;

        Assert.Equal(brush, sut.FieldBorderBrush);
    }

    [WpfFact]
    public void FieldBorderBrushWhenGotFocus_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.DarkGreen;
        sut.FieldBorderBrushWhenGotFocus = brush;

        Assert.Equal(brush, sut.FieldBorderBrushWhenGotFocus);
    }

    [WpfFact]
    public void FieldBorderBrushWhenIsDisabled_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.DarkBlue;
        sut.FieldBorderBrushWhenIsDisabled = brush;

        Assert.Equal(brush, sut.FieldBorderBrushWhenIsDisabled);
    }

    [WpfFact]
    public void FieldBorderCornerRadius_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        var radius = new CornerRadius(5);
        sut.FieldBorderCornerRadius = radius;

        Assert.Equal(radius, sut.FieldBorderCornerRadius);
    }

    [WpfFact]
    public void FieldBorderThickness_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        var thickness = new Thickness(2, 3, 2, 3);
        sut.FieldBorderThickness = thickness;

        Assert.Equal(thickness, sut.FieldBorderThickness);
    }

    [WpfFact]
    public void FieldForeground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.LightSalmon;
        sut.FieldForeground = brush;

        Assert.Equal(brush, sut.FieldForeground);
    }

    [WpfFact]
    public void FieldForegroundWhenGotFocus_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.LightGreen;
        sut.FieldForegroundWhenGotFocus = brush;

        Assert.Equal(brush, sut.FieldForegroundWhenGotFocus);
    }

    [WpfFact]
    public void FieldForegroundWhenIsDisabled_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.LightBlue;
        sut.FieldForegroundWhenIsDisabled = brush;

        Assert.Equal(brush, sut.FieldForegroundWhenIsDisabled);
    }

    [WpfFact]
    public void FieldHeight_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        double value = 100.5D;
        sut.FieldHeight = value;

        Assert.Equal(value, sut.FieldHeight);
    }

    [WpfFact]
    public void FieldHorizontalAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField
        {
            FieldHorizontalAlignment = HorizontalAlignment.Right
        };

        Assert.Equal(HorizontalAlignment.Right, sut.FieldHorizontalAlignment);
    }

    [WpfFact]
    public void FieldTextAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField
        {
            FieldTextAlignment = TextAlignment.Center
        };

        Assert.Equal(TextAlignment.Center, sut.FieldTextAlignment);
    }

    [WpfFact]
    public void FieldWidth_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        double value = 200.25D;
        sut.FieldWidth = value;

        Assert.Equal(value, sut.FieldWidth);
    }

    [WpfFact]
    public void LabelBackground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.Yellow;
        sut.LabelBackground = brush;

        Assert.Equal(brush, sut.LabelBackground);
    }

    [WpfFact]
    public void LabelForeground_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        SolidColorBrush brush = Brushes.Black;
        sut.LabelForeground = brush;

        Assert.Equal(brush, sut.LabelForeground);
    }

    [WpfFact]
    public void LabelHeight_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        double value = 50D;
        sut.LabelHeight = value;

        Assert.Equal(value, sut.LabelHeight);
    }

    [WpfFact]
    public void LabelMargin_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

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
        var sut = new TestEditField
        {
            LabelPosition = labelPosition
        };

        Assert.Equal(labelPosition, sut.LabelPosition);
    }

    [WpfFact]
    public void LabelText_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        string text = "LabelTextExample";
        sut.LabelText = text;

        Assert.Equal(text, sut.LabelText);
    }

    [WpfFact]
    public void LabelTextAlignment_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField
        {
            LabelTextAlignment = TextAlignment.Right
        };

        Assert.Equal(TextAlignment.Right, sut.LabelTextAlignment);
    }

    [WpfFact]
    public void LabelWidth_can_be_set_and_get_as_expected()
    {
        var sut = new TestEditField();

        double value = 75.75D;
        sut.LabelWidth = value;

        Assert.Equal(value, sut.LabelWidth);
    }
}
