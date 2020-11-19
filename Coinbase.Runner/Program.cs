﻿
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
            Console.WriteLine(">>> Starting >>>>");

            var serviceCollection = new ServiceCollection();

            var serviceProvider = ConfigureServices(serviceCollection);


            IBusiness business = serviceProvider.GetService<IBusiness>();

            var getCurrenciesTask = business.GetCurrenciesAsync(new string[] { "USD", "EUR" });
            getCurrenciesTask.Wait();

            var currencies = getCurrenciesTask.Result;

            Console.WriteLine(currencies.FirstOrDefault());
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