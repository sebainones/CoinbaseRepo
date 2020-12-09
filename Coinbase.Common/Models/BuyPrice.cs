using Newtonsoft.Json;

namespace Coinbase.Common.Models
{
    public class BuyPrice
    {
        [JsonProperty("base")]
        public string Base;

        [JsonProperty("currency")]
        public string Currency;

        [JsonProperty("amount")]
        public string Amount;
    }
}