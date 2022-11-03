using System;
using System.Collections.Generic;

namespace StuartDelivery.Models.Webhook.Enums
{
    public static class WebhookTopicExtensions
    {
        private static readonly Dictionary<WebhookTopic, string> _enumMap;
        private static readonly Dictionary<string, WebhookTopic> _stringMap;

        static WebhookTopicExtensions()
        {
            _enumMap = new Dictionary<WebhookTopic, string>
            {
                { WebhookTopic.JobCreate, "job/create" },
                { WebhookTopic.JobUpdate, "job/update" },
                { WebhookTopic.DeliveryCreate, "delivery/create" },
                { WebhookTopic.DeliveryUpdate, "delivery/update" },
                { WebhookTopic.DriverUpdate, "driver/update" },
                { WebhookTopic.DriverOnline, "driver/online" },
                { WebhookTopic.DriverOffline, "driver/offline" },
            };

            _stringMap = new Dictionary<string, WebhookTopic>(_enumMap.Count);
            foreach(var pair in _enumMap)
            {
                _stringMap[pair.Value] = pair.Key;
            }
        }

        public static string ToApiString(this WebhookTopic topic)
        {
            if(!_enumMap.ContainsKey(topic))
            {
                throw new ArgumentOutOfRangeException(nameof(topic), $"Webhook topic {topic} not valid");
            }

            return _enumMap[topic];
        }

        public static WebhookTopic FromApiString(this string s)
        {
            if (!_stringMap.ContainsKey(s))
            {
                throw new ArgumentOutOfRangeException(nameof(s), $"Webhook topic {s} not valid");
            }

            return _stringMap[s];
        }
    }
}
