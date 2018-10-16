using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class AccessCode
    {
        //Access code
        [JsonProperty(PropertyName = "code")]
        [JsonRequired]
        public string Code { get; set; }

        //Access code type Allowed Values: text, qr_text
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonRequired]
        public AccessCodeType Type { get; set; }

        //Access code title
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //Access code instructions
        [JsonProperty(PropertyName = "instructions")]
        public string Instructions { get; set; }
    }
}