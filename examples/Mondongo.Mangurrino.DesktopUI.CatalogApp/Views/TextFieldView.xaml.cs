// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFieldView.xaml.cs" company="OscarNET-SOFTware">
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

using System.Windows.Controls;

using Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp.Views;

internal partial class TextFieldView : UserControl
{
    public TextFieldView()
    {
        InitializeComponent();
    }

    public TextFieldView(TextFieldViewModel viewModel)
        : this()
    {
        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }
}
