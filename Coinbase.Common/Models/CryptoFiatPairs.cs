namespace Coinbase.Common.Models
{
    /// <summary>
    /// /// Some countries have access to crypto/fiat trading pairs while other countries can only access crypto/crypto trading pairs.
    /// https://help.coinbase.com/en/pro/trading-and-funding/cryptocurrency-trading-pairs/locations-and-trading-pairs
    /// </summary>
    public static class CryptoFiatPairs
    {
        // Crypto/Fiat
        public static string BTCUSD => "BTC-USD";
        public static string BTCEUR => "BTC-EUR";
        public static string ETHEUR => "ETH-EUR";
        public static string LTCEUR => "LTC-EUR";
    }
}
