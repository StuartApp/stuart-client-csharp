using System;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Slot
    {
        [JsonProperty(PropertyName = "start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTime EndTime { get; set; }
    }
}