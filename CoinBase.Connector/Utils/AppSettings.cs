using System.IO;
using Microsoft.Extensions.Configuration;
namespace CoinBase.Connector.Utils
{
    public class AppSettings
    {
        private static AppSettings appSettingsInstance;
        public static AppSettings HydratedAppSettings
        {
            get
            {
                if (appSettingsInstance is null)
                {
                    appSettingsInstance = new AppSettings();
                    InitiliazeSettings();
                }
                return appSettingsInstance;
            }
        }

        private AppSettings()
        {
            //To avoid being constructed from outside
        }

        private static void InitiliazeSettings()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var appSettingsSection = configuration.GetSection("AppSettings");

            ConfigurationBinder.Bind(appSettingsSection, appSettingsInstance);
        }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string MediaTypeJson { get; set; }
        public string ApiEndpoint { get; set; }
    }
}