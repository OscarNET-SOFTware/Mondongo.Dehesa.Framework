// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="OscarNET-SOFTware">
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

using Microsoft.Extensions.DependencyInjection;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

internal sealed class MainViewModel : ObservableObject
{
    private ITestControlViewModel? _selectedControl;

    public MainViewModel(IServiceProvider serviceProvider)
        : base()
    {
        ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

        Controls = [
            serviceProvider.GetRequiredService<TextFieldViewModel>(),
        ];

        SelectedControl = Controls.FirstOrDefault();
    }

    public ObservableCollection<ITestControlViewModel> Controls { get; }

    public ITestControlViewModel? SelectedControl
    {
        get => _selectedControl;
        set => SetProperty(ref _selectedControl, value);
    }
}
