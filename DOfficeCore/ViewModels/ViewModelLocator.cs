using Microsoft.Extensions.DependencyInjection;

namespace DOfficeCore.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
