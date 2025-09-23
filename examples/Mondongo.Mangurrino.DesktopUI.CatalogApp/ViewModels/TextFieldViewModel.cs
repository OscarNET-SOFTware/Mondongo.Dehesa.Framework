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
using System.Windows;
using System.Windows.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

internal sealed class TextFieldViewModel : ObservableObject, ITestControlViewModel
{
    private bool _isEnabled = true;

    private Brush _selectedFieldBackground = (Brush)EditFieldBase.FieldBackgroundProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldBackgroundWhenGotFocus = (Brush)EditFieldBase.FieldBackgroundWhenGotFocusProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldBackgroundWhenIsDisabled = (Brush)EditFieldBase.FieldBackgroundWhenIsDisabledProperty.DefaultMetadata.DefaultValue;

    private Brush _selectedFieldBorderBrush = (Brush)EditFieldBase.FieldBorderBrushProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldBorderBrushWhenGotFocus = (Brush)EditFieldBase.FieldBorderBrushWhenGotFocusProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldBorderBrushWhenIsDisabled = (Brush)EditFieldBase.FieldBorderBrushWhenIsDisabledProperty.DefaultMetadata.DefaultValue;

    private Brush _selectedFieldForeground = (Brush)EditFieldBase.FieldForegroundProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldForegroundWhenGotFocus = (Brush)EditFieldBase.FieldForegroundWhenGotFocusProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedFieldForegroundWhenIsDisabled = (Brush)EditFieldBase.FieldForegroundWhenIsDisabledProperty.DefaultMetadata.DefaultValue;

    private Brush _selectedLabelBackground = (Brush)EditFieldBase.LabelBackgroundProperty.DefaultMetadata.DefaultValue;
    private Brush _selectedLabelForeground = (Brush)EditFieldBase.LabelForegroundProperty.DefaultMetadata.DefaultValue;

    private EditFieldLabelPosition _selectedLabelPosition = (EditFieldLabelPosition)EditFieldBase.LabelPositionProperty.DefaultMetadata.DefaultValue;
    private TextAlignment _selectedLabelTextAlignment = (TextAlignment)EditFieldBase.LabelTextAlignmentProperty.DefaultMetadata.DefaultValue;
    private string _selectedLabelText = nameof(TextField);

    public TextFieldViewModel()
        : base()
    {
        LabelPositions = [.. Enum.GetValues<EditFieldLabelPosition>()];
        LabelTextAlignments = [.. Enum.GetValues<TextAlignment>()];
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public ObservableCollection<EditFieldLabelPosition> LabelPositions { get; }
    public ObservableCollection<TextAlignment> LabelTextAlignments { get; }

    public string Name => nameof(TextField);

    public Brush SelectedFieldBackground
    {
        get => _selectedFieldBackground;
        set => SetProperty(ref _selectedFieldBackground, value);
    }

    public Brush SelectedFieldBackgroundWhenGotFocus
    {
        get => _selectedFieldBackgroundWhenGotFocus;
        set => SetProperty(ref _selectedFieldBackgroundWhenGotFocus, value);
    }

    public Brush SelectedFieldBackgroundWhenIsDisabled
    {
        get => _selectedFieldBackgroundWhenIsDisabled;
        set => SetProperty(ref _selectedFieldBackgroundWhenIsDisabled, value);
    }

    public Brush SelectedFieldBorderBrush
    {
        get => _selectedFieldBorderBrush;
        set => SetProperty(ref _selectedFieldBorderBrush, value);
    }

    public Brush SelectedFieldBorderBrushWhenGotFocus
    {
        get => _selectedFieldBorderBrushWhenGotFocus;
        set => SetProperty(ref _selectedFieldBorderBrushWhenGotFocus, value);
    }

    public Brush SelectedFieldBorderBrushWhenIsDisabled
    {
        get => _selectedFieldBorderBrushWhenIsDisabled;
        set => SetProperty(ref _selectedFieldBorderBrushWhenIsDisabled, value);
    }

    public Brush SelectedFieldForeground
    {
        get => _selectedFieldForeground;
        set => SetProperty(ref _selectedFieldForeground, value);
    }

    public Brush SelectedFieldForegroundWhenGotFocus
    {
        get => _selectedFieldForegroundWhenGotFocus;
        set => SetProperty(ref _selectedFieldForegroundWhenGotFocus, value);
    }

    public Brush SelectedFieldForegroundWhenIsDisabled
    {
        get => _selectedFieldForegroundWhenIsDisabled;
        set => SetProperty(ref _selectedFieldForegroundWhenIsDisabled, value);
    }

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
}
