using System.Windows;
using Serilog;

namespace DOfficeCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    path: "Logs\\log.txt",
                    fileSizeLimitBytes: 104857600,
                    rollOnFileSizeLimit: true,
                    outputTemplate: "===> {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:l}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
