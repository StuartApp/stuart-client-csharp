using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Eta
    {
        /// <summary>
        /// The Delivery ETA to origin.
        /// </summary>
        [JsonProperty(PropertyName = "pickup")]
        public string PickUp { get; set; }

        /// <summary>
        /// The Delivery ETA to destination.
        /// </summary>
        [JsonProperty(PropertyName = "dropoff")]
        public string DropOff { get; set; }
    }
}