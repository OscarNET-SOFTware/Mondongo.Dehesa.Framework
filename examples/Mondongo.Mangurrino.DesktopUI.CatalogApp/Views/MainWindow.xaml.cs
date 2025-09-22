// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="OscarNET-SOFTware">
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
using System.Windows.Forms;
using System.Windows.Interop;

using Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.Views;

internal partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CenterWindow();
    }

    public MainWindow(MainViewModel viewModel)
        : this()
    {
        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    private void CenterWindow()
    {
        // Get the monitor where this window is located.
        var windowInteropHelper = new WindowInteropHelper(this);
        var screen = Screen.FromHandle(windowInteropHelper.Handle);

        // Consider DPI scaling.
        var source = PresentationSource.FromVisual(this);
        double dpiX = source?.CompositionTarget?.TransformToDevice.M11 ?? 1.0D;
        double dpiY = source?.CompositionTarget?.TransformToDevice.M22 ?? 1.0D;

        // Apply centered size and position.
        int margen = 50;
        Left = (screen.WorkingArea.Left + margen) / dpiX;
        Top = (screen.WorkingArea.Top + margen) / dpiY;
        Width = (screen.WorkingArea.Width - margen * 2) / dpiX;
        Height = (screen.WorkingArea.Height - margen * 2) / dpiY;
    }
}
