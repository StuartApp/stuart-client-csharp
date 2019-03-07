using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StuartDelivery.Models.Job.Response
{
    public class Slot
    {
        [JsonProperty(PropertyName = "start_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset EndTime { get; set; }
    }
}