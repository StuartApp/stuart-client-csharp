using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class AccessCode
    {
        /// <summary>
        /// Access code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        [JsonRequired]
        public string Code { get; set; }

        /// <summary>
        /// Access code type Allowed Values: text, qr_text
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonRequired]
        public AccessCodeType Type { get; set; }

        /// <summary>
        /// Access code title
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Access code instructions
        /// </summary>
        [JsonProperty(PropertyName = "instructions")]
        public string Instructions { get; set; }
    }
}