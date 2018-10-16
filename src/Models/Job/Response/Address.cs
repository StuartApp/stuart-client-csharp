using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Address
    {
        [JsonProperty(PropertyName = "street")]
        [JsonRequired]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "postcode")]
        [JsonRequired]
        public string PostCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        [JsonRequired]
        public string Country { get; set; }
    }
}
