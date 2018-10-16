using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class UpdatedJob
    {
        [JsonProperty(PropertyName = "deliveries")]
        public IEnumerable<Delivery> Deliveries { get; set; }
    }
}
