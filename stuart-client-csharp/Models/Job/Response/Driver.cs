using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Driver
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "picture_url")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "transport_type")]
        public string TransportType { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }
    }
}