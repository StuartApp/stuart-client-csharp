using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Delivery
    {
        /// <summary>
        /// The Delivery Id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        [JsonRequired]
        public int Id { get; set; }

        /// <summary>
        /// The Delivery status.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        [JsonRequired]
        public string Status { get; set; }

        /// <summary>
        /// A datetime (ISO 8601) indicating when your package has been picked.
        /// </summary>
        [JsonProperty(PropertyName = "picked_at")]
        public string PickedAt { get; set; }

        /// <summary>
        /// A datetime(ISO 8601) indicating when your package has been delivered.
        /// </summary>
        [JsonProperty(PropertyName = "delivered_at")]
        public string DeliveredAt { get; set; }

        /// <summary>
        /// The Delivery tracking URL.
        /// </summary>
        [JsonProperty(PropertyName = "tracking_url")]
        [JsonRequired]
        public string TrackingUrl { get; set; }

        [JsonProperty(PropertyName = "client_reference")]
        public string ClientReference { get; set; }

        [JsonProperty(PropertyName = "package_description")]
        public string PackageDescription { get; set; }

        [JsonProperty(PropertyName = "package_type")]
        [JsonRequired]
        public string PackageType { get; set; }

        [JsonProperty(PropertyName = "pickup")]
        [JsonRequired]
        public Location PickUp { get; set; }

        [JsonProperty(PropertyName = "dropoff")]
        [JsonRequired]
        public Location DropOff { get; set; }

        [JsonProperty(PropertyName = "eta")]
        [JsonRequired]
        public Eta Eta { get; set; }

        [JsonProperty(PropertyName = "cancellation")]
        public Cancellation Cancellation { get; set; }

        public Proof Proof { get; set; }
    }
}