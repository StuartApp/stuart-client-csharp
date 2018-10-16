using Newtonsoft.Json;

namespace StuartDelivery.Models.Address
{
    public class Property
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}