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
            var result = await coinBaseClient.MakeNormalRequestCallAsync<Response>( @"/v2/currencies/", string.Empty);

            return result.Data.Where(c => currenciesName.Contains(c.Id)).ToList();
        }

        //coinBaseClient.MakeAuthorizedRequestCall(RestMethods.GET, @"/v2/exchange-rates?currency=USD", string.Empty);
    }
}
