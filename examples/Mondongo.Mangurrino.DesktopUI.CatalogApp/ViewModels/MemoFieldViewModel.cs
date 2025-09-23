// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoFieldViewModel.cs" company="OscarNET-SOFTware">
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

using CommunityToolkit.Mvvm.ComponentModel;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

internal sealed class MemoFieldViewModel : ObservableObject, ITestControlViewModel
{
    private bool _isEnabled = true;
    private EditFieldLabelPosition _selectedLabelPosition = (EditFieldLabelPosition)EditFieldBase.LabelPositionProperty.DefaultMetadata.DefaultValue;

    public MemoFieldViewModel()
        : base()
    {
        LabelPositions = [.. Enum.GetValues<EditFieldLabelPosition>()];
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public ObservableCollection<EditFieldLabelPosition> LabelPositions { get; }

    public string Name => nameof(MemoField);

    public EditFieldLabelPosition SelectedLabelPosition
    {
        get => _selectedLabelPosition;
        set => SetProperty(ref _selectedLabelPosition, value);
    }
}
