using Serilog;
using System.Windows;

namespace MailSender
{
    public partial class App : Application
    {
        public App()
        {
            ConfigureLogging();
        }

        private void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Приложение WPF запускается.");
        }
    }
}
