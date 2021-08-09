using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StuartDelivery.Models.Webhook.Request
{
    public class Webhook
    {
        [JsonProperty(PropertyName = "url")]
        [JsonRequired]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "topics")]
        [JsonRequired]
        public string[] Topics { get; set; } = new string[0];

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "authentication_header")]
        public string AuthenticationHeader { get; set; }

        [JsonProperty(PropertyName = "authentication_key")]
        public string AuthenticationKey { get; set; }
    }
}
