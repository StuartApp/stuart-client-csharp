using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class Job
    {
        //A datetime(ISO 8601) indicating when you want the package to be picked.
        //Must be between the current time and 30 days in the future.Not to be specified if you already specify a dropoff_at.
        [JsonProperty(PropertyName = "pickup_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? PickUpAt { get; set; }

        //A datetime (ISO 8601) indicating when you want the package to be delivered.
        //Must be between 60 min ahead of the current time and 30 days in the future.Not to be specified if you already specify a pickup_at.
        [JsonProperty(PropertyName = "dropoff_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? DropOffAt { get; set; }

        //Accounting reference number.
        [JsonProperty(PropertyName = "assigment_code")]
        public string AssigmentCode { get; set; }

        //🇫🇷 France only Which transport type you want for your Job. Mandatory if package_type is empty.
        [JsonProperty(PropertyName = "transport_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransportType? TransportType { get; set; }

        [JsonProperty(PropertyName = "pickups")]
        [JsonRequired]
        public IEnumerable<PickUp> PickUps { get; set; }

        [JsonProperty(PropertyName = "dropoffs")]
        [JsonRequired]
        public IEnumerable<DropOff> DropOffs { get; set; }
    }
}
