using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class JobRequest
    {
        [JsonProperty(PropertyName = "job")]
        public Job Job { get; set; }
    }
}
