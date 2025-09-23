// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BrushSelector.xaml.cs" company="OscarNET-SOFTware">
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
using System.Windows.Media;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.Controls;

internal partial class BrushSelector : UserControl
{
    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register(
            name: nameof(Caption),
            propertyType: typeof(string),
            ownerType: typeof(BrushSelector),
            typeMetadata: new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(
            name: nameof(SelectedValue),
            propertyType: typeof(Brush),
            ownerType: typeof(BrushSelector),
            typeMetadata: new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public BrushSelector()
    {
        InitializeComponent();
    }

    public List<KeyValuePair<string, Brush>> AllBrushes { get; } = [.. DesktopElements.AllBrushes];

    public string Caption
    {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

    public Brush SelectedValue
    {
        get => (Brush)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }
}
