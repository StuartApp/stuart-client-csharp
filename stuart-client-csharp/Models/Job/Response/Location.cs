using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Location
    {
        [JsonProperty(PropertyName = "id")]
        [JsonRequired]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        [JsonRequired]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        [JsonRequired]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "address")]
        [JsonRequired]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "contact")]
        [JsonRequired]
        public Contact Contact { get; set; }
    }
}