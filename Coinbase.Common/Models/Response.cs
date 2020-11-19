using Newtonsoft.Json;
using System.Collections.Generic;

namespace Coinbase.Common.Models
{
    public class Response
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Currency> Data { get; set; }
    }
}
