using Coinbase.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinBase.Business
{
    public interface IBusiness
    {
        Task<List<Currency>> GetCurrenciesAsync(string[] currenciesName);

        /// <summary>
        /// Get current exchange rates.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<ExchangeRate> GetExchangeRatesAsync(string currency);

        /// <summary>
        /// Get the total price to buy one bitcoin or ether.
        /// https://help.coinbase.com/en/pro/trading-and-funding/cryptocurrency-trading-pairs/locations-and-trading-pairs
        // Some countries have access to crypto/fiat trading pairs while other countries can only access crypto/crypto trading pairs.
        /// </summary>
        /// <param name="currenciesPair"></param>
        /// <returns></returns>
        Task<BuyPrice> GetBuyPriceAsync(string currenciesPair);
    }
}