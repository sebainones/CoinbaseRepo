using Newtonsoft.Json;
using System.Collections.Generic;

namespace Coinbase.Common.Models
{
    public class ExchangeRate
    {
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("rates", NullValueHandling = NullValueHandling.Ignore)]
        public Rates Rates { get; set; }
    }
}