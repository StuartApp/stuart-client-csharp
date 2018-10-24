using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class Contact
    {
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}