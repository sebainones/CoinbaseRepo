using Coinbase.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinBase.Business
{
    public interface IBusiness
    {
        Task<List<Currency>> GetCurrenciesAsync(string[] currenciesName);

        Task<ExchangeRate> GetExchangeRatesAsync(string cuurency);

        Task<BuyPrice> GetBuyPriceAsync(string currenciesPair);
    }
}