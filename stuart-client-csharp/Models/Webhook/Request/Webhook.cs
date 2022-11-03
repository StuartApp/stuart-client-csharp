using Newtonsoft.Json;
using StuartDelivery.Models.Webhook.Enums;

namespace StuartDelivery.Models.Webhook.Request
{
    public class Webhook
    {
        [JsonProperty(PropertyName = "url")]
        [JsonRequired]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "topics")]
        [JsonRequired]
        [JsonConverter(typeof(WebhookTopicArrayJsonConverter))]
        public WebhookTopic[] Topics { get; set; } = new WebhookTopic[0];

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "authentication_header")]
        public string AuthenticationHeader { get; set; }

        [JsonProperty(PropertyName = "authentication_key")]
        public string AuthenticationKey { get; set; }
    }
}
