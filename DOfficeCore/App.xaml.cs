using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace DOfficeCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _Hosting;
        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
                    .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                    .ConfigureServices(ConfigureServices)
                    .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<IDataProviderService, DataProviderService>();
            services.AddSingleton<IViewCollectionProvider, ViewCollectionProvider>();
            services.AddSingleton<IDiaryBoxProvider, DiaryBoxProvider>();
            services.AddSingleton<ILineEditorService, LineEditorService>();
            services.AddSingleton<ICollectionHandler, CollectionHandler>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
