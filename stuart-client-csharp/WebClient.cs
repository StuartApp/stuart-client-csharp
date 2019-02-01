using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StuartDelivery
{
    public class WebClient
    {
        private readonly HttpClient _client;

        public WebClient(Environment environment)
        {
            _client = new HttpClient { BaseAddress = new Uri($"{environment.BaseUrl}") };
            _client.DefaultRequestHeaders.Add("User-Agent", "stuart-client-csharp/1.3.1");
        }

        public void SetAuthorization(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _client.SendAsync(CreateRequest(HttpMethod.Get, uri, null));
        }

        public async Task<HttpResponseMessage> PostAsync<TModel>(string uri, TModel model)
        {
            return await _client.SendAsync(CreateRequest(HttpMethod.Post, uri, model));
        }

        public async Task<HttpResponseMessage> PostAsync(string uri)
        {
            return await _client.SendAsync(CreateRequest(HttpMethod.Post, uri, null));
        }

        public async Task<HttpResponseMessage> PutAsync<TModel>(string uri, TModel model)
        {
            return await _client.SendAsync(CreateRequest(HttpMethod.Put, uri, model));
        }

        public async Task<HttpResponseMessage> PatchAsync<TModel>(string uri, TModel model)
        {
            return await _client.SendAsync(CreateRequest(new HttpMethod("PATCH"), uri, model));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await _client.SendAsync(CreateRequest(HttpMethod.Delete, uri, null));
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object data)
        {
            var request = new HttpRequestMessage(method, uri);
            if (data != null)
            {
                request.Content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter
                {
                    SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                });
            }

            return request;
        }
    }
}
