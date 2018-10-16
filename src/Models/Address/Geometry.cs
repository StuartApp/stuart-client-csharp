using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class Geometry
    {
        [JsonProperty(PropertyName = "coordinates")]
        public IEnumerable<IEnumerable<double[]>> Coordinates { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}