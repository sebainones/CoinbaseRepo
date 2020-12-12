using System.Windows;
using Coinbase.ClientApp.ViewModels;
using Coinbase.ClientApp.Views;
using Coinbase.Connector.Services;
using CoinBase.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Coinbase.ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            _serviceProvider = ConfigureServices(serviceCollection);
        }

        private static ServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBusiness, Business>();
            services.AddSingleton<ICoinBaseClient, CoinBaseClient>();

            services.AddScoped<IMainWindowViewModel, MainWindowViewModel>();
            services.AddScoped<MainWindow>();

            //we will configure logging here
            return services
                     .AddLogging()
                     .BuildServiceProvider();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _serviceProvider.GetService<MainWindow>().Show();
        }
    }
}
