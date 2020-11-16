using System;
using Coinbase.Connector.Services;
using CoinBase.Connector.Utils;


namespace Coinbase.Connector
{
    class Program
    {

        static void Main(string[] args)
        {
            // The rest of your program here
            
            Console.WriteLine(AppSettings.HydratedAppSettings.ApiKey);

            var coninBaseClient = new CoinBaseClient();
            coninBaseClient.MakeRequestCall(RestMethods.GET, @"/v2/exchange-rates?currency=USD", string.Empty);
        }

        //TODO: review is this can be self contained in AppSettings class.
       
    }
}