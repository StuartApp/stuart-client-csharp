using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class Delivery
    {
        [JsonProperty(PropertyName = "id")]
        [JsonRequired]
        public int Id { get; set; }

        //Unique client order reference number.
        [JsonProperty(PropertyName = "client_reference")]
        public string ClientReference { get; set; }

        //A string that allows you to give more information on the package itself so it can be identified better.
        [JsonProperty(PropertyName = "package_description")]
        public string PackageDescription { get; set; }

        [JsonProperty(PropertyName = "pickup")]
        public PickUp PickUp { get; set; }

        [JsonProperty(PropertyName = "dropoff")]
        public DropOff DropOff { get; set; }
    }
}