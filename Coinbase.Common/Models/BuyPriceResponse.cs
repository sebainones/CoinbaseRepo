using Newtonsoft.Json;

namespace Coinbase.Common.Models
{
    public class BuyPriceResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public BuyPrice Data { get; set; }
    }
}
