// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="OscarNET-SOFTware">
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

using Microsoft.Extensions.DependencyInjection;

using Mondongo.Mangurrino.DesktopUI.CatalogApp.ViewModels;
using Mondongo.Mangurrino.DesktopUI.CatalogApp.Views;

namespace Mondongo.Mangurrino.DesktopUI.CatalogApp;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
        : base()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<MemoFieldView>();
        services.AddTransient<TextFieldView>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<MemoFieldViewModel>();
        services.AddTransient<TextFieldViewModel>();
    }
}
