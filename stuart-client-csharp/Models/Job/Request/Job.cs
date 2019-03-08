using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class Job
    {
        /// <summary>
        /// A datetime(ISO 8601) indicating when you want the package to be picked.
        /// Must be between the current time and 30 days in the future.Not to be specified if you already specify a dropoff_at.
        /// </summary>
        [JsonProperty(PropertyName = "pickup_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? PickUpAt { get; set; }

        /// <summary>
        /// A datetime (ISO 8601) indicating when you want the package to be delivered.
        /// Must be between 60 min ahead of the current time and 30 days in the future.Not to be specified if you already specify a pickup_at.
        /// </summary>
        [JsonProperty(PropertyName = "dropoff_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? DropOffAt { get; set; }

        /// <summary>
        /// Accounting reference number.
        /// </summary>
        [JsonProperty(PropertyName = "assignment_code")]
        public string AssignmentCode { get; set; }

        /// <summary>
        /// 🇫🇷 France only Which transport type you want for your Job. Mandatory if package_type is empty.
        /// </summary>
        [JsonProperty(PropertyName = "transport_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransportType? TransportType { get; set; }

        /// <summary>
        /// The origins of the deliveries.
        /// </summary>
        [JsonProperty(PropertyName = "pickups")]
        [JsonRequired]
        public IEnumerable<PickUp> PickUps { get; set; }

        /// <summary>
        /// The destinations of the deliveries.
        /// </summary>
        [JsonProperty(PropertyName = "dropoffs")]
        [JsonRequired]
        public IEnumerable<DropOff> DropOffs { get; set; }
    }
}
