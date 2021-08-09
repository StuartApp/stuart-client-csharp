using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        // NOTE: Ignoring property for now because the Stuart API returns data inconsistent with the documentation
        //       (i.e., should be array of strings, but is array of objects).
        [JsonProperty(PropertyName = "topics")]
        [JsonIgnore]
        public string[] Topics { get; set; } = new string[0];
    }
}
