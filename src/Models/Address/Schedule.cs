using System;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class Schedule
    {
        [JsonProperty(PropertyName = "parcel_shop")]
        public ParcelShop ParcelShop { get; set; }

        [JsonProperty(PropertyName = "from")]
        public DateTime? From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public DateTime? To { get; set; }
    }
}