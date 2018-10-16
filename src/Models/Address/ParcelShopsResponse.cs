using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class ParcelShopsResponse
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "schedule")]
        public IEnumerable<Schedule> Schedule { get; set; }
    }
}
