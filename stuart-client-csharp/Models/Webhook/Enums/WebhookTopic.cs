using System;
using System.Collections.Generic;
using System.Text;

namespace StuartDelivery.Models.Webhook.Enums
{
    public enum WebhookTopic
    {
        JobCreate,
        JobUpdate,
        DeliveryCreate,
        DeliveryUpdate,
        DriverUpdate,
        DriverOnline,
        DriverOffline
    }
}
