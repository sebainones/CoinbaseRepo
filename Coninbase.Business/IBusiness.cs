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

        Task<BuyPrice> GetBuyPriceAsync(string currenciesPair);
    }
}