// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFieldViewModel.cs" company="OscarNET-SOFTware">
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

using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

internal sealed class TextFieldViewModel : ObservableObject, ITestControlViewModel
{
    private string _displayName = "Text Field";
    private bool _isEnabled = true;

    private Brush _selectedLabelBackground = (Brush)EditFieldBase.LabelBackgroundProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedLabelForeground = (Brush)EditFieldBase.LabelForegroundProperty.DefaultMetadata.DefaultValue;
    private EditFieldLabelPosition _selectedLabelPosition = (EditFieldLabelPosition)EditFieldBase.LabelPositionProperty.DefaultMetadata.DefaultValue;
    private TextAlignment _selectedLabelTextAlignment = (TextAlignment)EditFieldBase.LabelTextAlignmentProperty.DefaultMetadata.DefaultValue;
    private string _selectedLabelText = nameof(TextField);

    public TextFieldViewModel()
        : base()
    {
        AllBrushes = [.. typeof(Brushes)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Select(p => new KeyValuePair<string, Brush>(p.Name, (Brush)p.GetValue(null, null)!))];

        LabelPositions = [.. Enum.GetValues<EditFieldLabelPosition>()];
        LabelTextAlignments = [.. Enum.GetValues<TextAlignment>()];
    }

    public List<KeyValuePair<string, Brush>> AllBrushes { get; }

    public ObservableCollection<EditFieldLabelPosition> LabelPositions { get; }
    public ObservableCollection<TextAlignment> LabelTextAlignments { get; }

    public Brush SelectedLabelBackground
    {
        get => _selectedLabelBackground;
        set => SetProperty(ref _selectedLabelBackground, value);
    }

    public Brush SelectedLabelForeground
    {
        get => _selectedLabelForeground;
        set => SetProperty(ref _selectedLabelForeground, value);
    }

    public EditFieldLabelPosition SelectedLabelPosition
    {
        get => _selectedLabelPosition;
        set => SetProperty(ref _selectedLabelPosition, value);
    }

    public TextAlignment SelectedLabelTextAlignment
    {
        get => _selectedLabelTextAlignment;
        set => SetProperty(ref _selectedLabelTextAlignment, value);
    }

    public string SelectedLabelText
    {
        get => _selectedLabelText;
        set => SetProperty(ref _selectedLabelText, value);
    }

    public string DisplayName
    {
        get => _displayName;
        set => SetProperty(ref _displayName, value);
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public string Name => "TextField";
}
