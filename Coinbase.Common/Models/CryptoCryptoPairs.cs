using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Common.Models
{
    /// <summary>
    /// Some countries have access to crypto/fiat trading pairs while other countries can only access crypto/crypto trading pairs.
    /// /// https://help.coinbase.com/en/pro/trading-and-funding/cryptocurrency-trading-pairs/locations-and-trading-pairs
    /// </summary>
    public static class CryptoCryptoPairs
    {
        public static string ETHBTC => "ETH-BTC";
        public static string BTCETH => "BTC-ETH";
    }
}
