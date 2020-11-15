using System;
using System.IO;
using Coinbase.Connector.Services;
using CoinBase.Connector.Utils;
using Microsoft.Extensions.Configuration;

namespace Coinbase.Connector {
    class Program {
        static AppSettings appSettings = new AppSettings ();
        static void Main (string[] args) {
            var builder = new ConfigurationBuilder()
                 .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true);
            var configuration = builder.Build ();
            ConfigurationBinder.Bind (configuration.GetSection ("AppSettings"), appSettings);
            // The rest of your program here

            Console.WriteLine(appSettings.MediaTypeJson);

            var coninBaseClient = new CoinBaseClient( appSettings);
            coninBaseClient.CreateRequestHeader();

        }

       
    }
}