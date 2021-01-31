
using Coinbase.Common.Models;
using Coinbase.Connector.Services;
using CoinBase.Business;
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
            Console.WriteLine(">>> Starting >>>>");

            var serviceCollection = new ServiceCollection();

            var serviceProvider = ConfigureServices(serviceCollection);

            IBusiness business = serviceProvider.GetService<IBusiness>();
            BusinessQueriesToBeRequestByUI(business);
        }

        private static void BusinessQueriesToBeRequestByUI(IBusiness business)
        {
            var getCurrenciesTask = business.GetCurrenciesAsync(new string[] { Currencies.USD, Currencies.EUR });
            getCurrenciesTask.Wait();
            var currencies = getCurrenciesTask.Result;
            Console.WriteLine(currencies.FirstOrDefault());

            var getCurrencyExchangeTask = business.GetExchangeRatesAsync(Currencies.EUR);
            var exchangeRateData = getCurrencyExchangeTask.Result;
            Console.WriteLine($"1 {Currencies.EUR} = {exchangeRateData.Rates[Currencies.USD]} {Currencies.USD}");

            BuyPrice btcUsdBuyPrice = business.GetBuyPriceAsync(CryptoFiatPairs.BTCUSD).Result;
            Console.WriteLine($"1 {btcUsdBuyPrice.Base} = { btcUsdBuyPrice.Amount} {btcUsdBuyPrice.Currency}");
        }

        private static ServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IBusiness, Business>();
            services.AddSingleton<ICoinBaseClient, CoinBaseClient>();

            //we will configure logging here
            return services
                     .AddLogging(configure => configure.AddConsole()) //added Console Logging!
                     .BuildServiceProvider();
        }
    }
}