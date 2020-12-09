using Newtonsoft.Json;

namespace Coinbase.Common.Models
{
    public class ResponseOf<T>
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
}
