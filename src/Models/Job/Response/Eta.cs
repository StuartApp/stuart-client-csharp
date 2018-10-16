using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Eta
    {
        //The Delivery ETA to origin.
        [JsonProperty(PropertyName = "pickup")]
        public string PickUp { get; set; }

        //The Delivery ETA to destination.
        [JsonProperty(PropertyName = "dropoff")]
        public string DropOff { get; set; }
    }
}