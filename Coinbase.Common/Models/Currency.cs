using Newtonsoft.Json;

namespace Coinbase.Common.Models
{
    public class Currency
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("min_size", NullValueHandling = NullValueHandling.Ignore)]
        public string MinSize { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}  \n Name: {Name} \n Minsize : {MinSize} ";
        }
    }
}
