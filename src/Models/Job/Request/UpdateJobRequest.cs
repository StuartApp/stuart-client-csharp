using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class UpdateJobRequest
    {
        [JsonProperty(PropertyName = "job")]
        public UpdatedJob Job { get; set; }
    }
}