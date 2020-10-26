using DOfficeCore.Logger;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels;
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
        public static IHost Hosting
        {
            get
            {
                if (_Hosting != null) return _Hosting;
                var host_builder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());

                host_builder.ConfigureServices(ConfigureServices);

                return _Hosting = host_builder.Build();
            }
        }

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices (HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<IDataProviderService, DataProviderService>();
            services.AddSingleton<IViewCollectionProvider, ViewCollectionProvider>();
            services.AddSingleton<IDiaryBoxProvider, DiaryBoxProvider>();
            services.AddSingleton<ILogger, Logger.Logger>();
            services.AddSingleton<ILineEditorService, LineEditorService>();

        }
    }
}
