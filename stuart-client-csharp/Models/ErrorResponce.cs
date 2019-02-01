using Newtonsoft.Json;

namespace StuartDelivery.Models
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
