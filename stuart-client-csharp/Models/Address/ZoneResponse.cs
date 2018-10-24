using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class ZoneResponse
    {
        [JsonProperty(PropertyName = "features")]
        public IEnumerable<Feature> Features { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
