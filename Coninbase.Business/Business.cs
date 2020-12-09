using Coinbase.Common.Models;
using Coinbase.Connector.Services;
using CoinBase.Connector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBase.Business
{
    //https://developers.coinbase.com/api/v2
    /// <summary>
    /// Business class to interecat with CoinBase endpoint API
    // Coinbase provides a simple and powerful REST API to integrate
    // bitcoin, bitcoin cash, litecoin and ethereum payments into your application.
    /// </summary>
    public class Business : IBusiness
    {
        private readonly ICoinBaseClient coinBaseClient;

        public Business(ICoinBaseClient coinBaseClient)
        {
            this.coinBaseClient = coinBaseClient ?? throw new ArgumentNullException(nameof(coinBaseClient));
        }

        public async Task<BuyPrice> GetBuyPriceAsync(string currenciesPair)
        {
            ///v2/prices/BTC-USD/buy
            var result = await coinBaseClient.MakeAuthorizedRequestCall<ResponseOf<BuyPrice>>(@$"/v2/prices/{currenciesPair}/buy", string.Empty);
            return result.Data;
        }

        public async Task<List<Currency>> GetCurrenciesAsync(string[] currenciesName)
        {
            var result = await coinBaseClient.MakeNormalRequestCallAsync<ResponseOf<List<Currency>>>(@"/v2/currencies/", string.Empty);
            return result.Data.Where(c => currenciesName.Contains(c.Id)).ToList();
        }

        public async Task<ExchangeRate> GetExchangeRatesAsync(string currency)
        {
            var result = await coinBaseClient.MakeNormalRequestCallAsync<ResponseOf<ExchangeRate>>(@$"/v2/exchange-rates?currency={currency}", string.Empty);

            return result.Data;
        }
    }
}
