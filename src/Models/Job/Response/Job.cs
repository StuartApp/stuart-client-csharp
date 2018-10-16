using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Response
{
    public class Job
    {
        [JsonProperty(PropertyName = "id")]
        [JsonRequired]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonRequired]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "package_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageSizeType? PackageType { get; set; }

        [JsonProperty(PropertyName = "transport_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransportType? TransportType { get; set; }

        [JsonProperty(PropertyName = "assignment_code")]
        public string AssignmentCode { get; set; }

        [JsonProperty(PropertyName = "pickup_at")]
        public string PickupAt { get; set; }

        [JsonProperty(PropertyName = "dropoff_at")]
        public string DropoffAt { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        //Total Job distance, in kilometer.
        [JsonProperty(PropertyName = "distance")]
        [JsonRequired]
        public double Distance { get; set; }

        //Total Job duration estimation, in minute.
        [JsonProperty(PropertyName = "duration")]
        [JsonRequired]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "deliveries")]
        [JsonRequired]
        public IEnumerable<Delivery> Deliveries { get; set; }

        [JsonProperty(PropertyName = "pricing")]
        [JsonRequired]
        public Pricing Pricing { get; set; }

        [JsonProperty(PropertyName = "driver")]
        [JsonRequired]
        public Driver Driver { get; set; }
    }
}
