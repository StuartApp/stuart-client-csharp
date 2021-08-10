using Newtonsoft.Json;
using StuartDelivery.Models.Webhook.Enums;
using System;
using System.Linq;

namespace StuartDelivery.Models.Webhook.Response
{
    public class Webhook
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "authentication_header")]
        public string AuthenticationHeader { get; set; }

        [JsonProperty(PropertyName = "authentication_key")]
        public string AuthenticationKey { get; set; }

        [JsonProperty(PropertyName = "topics")]
        private TopicContainer[] InnerTopics { get; set; } = new TopicContainer[0];

        private class TopicContainer
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        [JsonIgnore]
        public WebhookTopic[] Topics { get
            {
                return (InnerTopics ?? new TopicContainer[0])
                    .Select(t => t.Name.FromApiString())
                    .ToArray();
            }
        }
    }
}
