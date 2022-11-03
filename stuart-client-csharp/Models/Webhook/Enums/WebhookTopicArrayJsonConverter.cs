using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StuartDelivery.Models.Webhook.Enums
{
    class WebhookTopicArrayJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if(typeof(WebhookTopic[]).IsAssignableFrom(objectType))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WebhookTopic[] arr = (WebhookTopic[])value;
            writer.WriteStartArray();
            for(int i = 0; i < arr.Length; ++i)
            {
                writer.WriteValue(arr[i].ToApiString());
            }
            writer.WriteEndArray();
        }
    }
}
