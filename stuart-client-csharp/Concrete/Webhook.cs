using StuartDelivery.Abstract;
using StuartDelivery.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebhookRequest = StuartDelivery.Models.Webhook.Request;
using WebhookResponse = StuartDelivery.Models.Webhook.Response;

namespace StuartDelivery.Concrete
{
    class Webhook : IWebhook
    {
        private readonly WebClient _webClient;

        public Webhook(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<Result<WebhookResponse.Webhook>> Create(WebhookRequest.Webhook webhook)
        {
            try
            {
                var response = await _webClient.PostAsync("/v2/webhooks", webhook).ConfigureAwait(false);
                var result = new Result<WebhookResponse.Webhook>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<WebhookResponse.Webhook>().ConfigureAwait(false);
                    return result;
                }

                result.Error = await response.Content.ReadAsAsync<ErrorResponse>();
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(Create)} failed", e);
            }
        }

        public async Task<Result<bool>> Delete(int webhookId)
        {
            try
            {
                var response = await _webClient.DeleteAsync($"/v2/webhooks/{webhookId}").ConfigureAwait(false);
                var result = new Result<bool>();

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                    result.Data = (bool)obj.FirstOrDefault(x => x.Key == "success").Value;
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(Delete)} failed", e);
            }
        }

        public async Task<Result<WebhookResponse.Webhook>> Get(int webhookId)
        {
            try
            {
                var response = await _webClient.GetAsync($"/v2/webhooks/{webhookId}").ConfigureAwait(false);
                var result = new Result<WebhookResponse.Webhook>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<WebhookResponse.Webhook>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(Get)} failed", e);
            }
        }

        public async Task<Result<WebhookResponse.Webhook[]>> GetList()
        {
            try
            {
                var response = await _webClient.GetAsync($"/v2/webhooks").ConfigureAwait(false);
                var result = new Result<WebhookResponse.Webhook[]>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<WebhookResponse.Webhook[]>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetList)} failed", e);
            }
        }

        public async Task<Result<WebhookResponse.Webhook>> Update(int webhookId, WebhookRequest.Webhook webhook)
        {
            try
            {
                var response = await _webClient.PutAsync($"/v2/webhooks/{webhookId}", webhook).ConfigureAwait(false);
                var result = new Result<WebhookResponse.Webhook>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<WebhookResponse.Webhook>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(Update)} failed", e);
            }
        }
    }
}
