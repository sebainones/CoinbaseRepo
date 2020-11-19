
using Coinbase.Connector.Services;
using CoinBase.Business;
using CoinBase.Connector.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coinbase.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(AppSettings.HydratedAppSettings.ApiKey);

            //ConsoleLoggerExtensions.AddConsole(new ConsoleLogger())

            //TODO:  use DI framework
            var serviceProvider = new ServiceCollection()
                      .AddLogging() //<-- You were missing this
                      .BuildServiceProvider();
            //get logger
            var logger = serviceProvider.GetService<ILoggerFactory>()
                        .CreateLogger<Program>();


            IBusiness business = new Business(new CoinBaseClient());

            var getCurrenciesTask = business.GetCurrenciesAsync(new string[] { "USD", "EUR" });

            getCurrenciesTask.Wait();

            var currencies = getCurrenciesTask.Result;

            Console.WriteLine(currencies.FirstOrDefault());
        }
    }
}