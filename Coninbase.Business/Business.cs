using Coinbase.Common.Models;
using Coinbase.Connector.Services;
using CoinBase.Connector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBase.Business
{
    public class Business : IBusiness
    {
        private readonly ICoinBaseClient coinBaseClient;

        public Business(ICoinBaseClient coinBaseClient)
        {
            this.coinBaseClient = coinBaseClient ?? throw new ArgumentNullException(nameof(coinBaseClient));
        }
        public async Task<List<Currency>> GetCurrenciesAsync(string[] currenciesName)
        {
            coinBaseClient.MakeAuthorizedRequestCall(RestMethods.GET, @"/v2/exchange-rates?currency=USD", string.Empty);

            var result = await coinBaseClient.MakeNormalRequestCallAsync<Response>(RestMethods.GET, @"/v2/currencies/", string.Empty);

            foreach (var currency in result.Data)
            {
                if (currenciesName.Contains(currency.Id))
                    Console.WriteLine(currency.Name);

            }

            return result.Data.Where(c => currenciesName.Contains(c.Id)).ToList();
        }
    }
}
