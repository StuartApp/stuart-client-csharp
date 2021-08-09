using StuartDelivery.Models;
using System.Threading.Tasks;
using WebhookRequest = StuartDelivery.Models.Webhook.Request;
using WebhookResponse = StuartDelivery.Models.Webhook.Response;

namespace StuartDelivery.Abstract
{
    public interface IWebhook
    {
        /// <summary>
        /// This endpoint allows you to create a webhook through the Stuart API.
        /// </summary>
        Task<Result<WebhookResponse.Webhook>> Create(WebhookRequest.Webhook webhook);

        /// <summary>
        /// This endpoint will return a list of your current webhooks.
        /// Currently, you are able to have a maximum of 6 webhooks.
        /// </summary>
        Task<Result<WebhookResponse.Webhook[]>> GetList();

        /// <summary>
        /// This endpoint allows you to get information on a specific webhook that you have
        /// created using the <see cref="Create(WebhookRequest.Webhook)"/> method.
        /// </summary>
        Task<Result<WebhookResponse.Webhook>> Get(int webhookId);

        /// <summary>
        /// This endpoint allows you to update any webhooks you have created.
        /// </summary>
        Task<Result<WebhookResponse.Webhook>> Update(int webhookId, WebhookRequest.Webhook webhook);

        /// <summary>
        /// This endpoint allows you to delete a particular webhook.
        /// </summary>
        Task<Result<bool>> Delete(int webhookId);
    }
}
