using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Cancellation
    {
        [JsonProperty(PropertyName = "canceled_by")]
        public string CanceledBy { get; set; }

        [JsonProperty(PropertyName = "reason_key")]
        public string ReasonKey { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}