using Newtonsoft.Json;
using System.Collections.Generic;

namespace Coinbase.Common.Models
{
    public class CurrenciesResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Currency> Data { get; set; }
    }
}
