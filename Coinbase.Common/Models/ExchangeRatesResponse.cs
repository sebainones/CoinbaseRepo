using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Common.Models
{
    public class ExchangeRatesResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public ExchangeRateData Data { get; set; }
    }
}
