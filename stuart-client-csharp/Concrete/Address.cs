using System;
using System.Threading.Tasks;
using StuartDelivery.Abstract;
using StuartDelivery.Models.Address;
using System.Net;
using System.Net.Http;
using StuartDelivery.Models;
using System.Dynamic;
using System.Linq;

namespace StuartDelivery.Concrete
{
    public class Address : IAddress
    {
        private WebClient _webClient;

        public Address(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<ParcelShopsResponse> GetParcelShops(string address, DateTime date)
        {
            var urlParams = $"?address={WebUtility.UrlEncode(address)}&date={date.ToString("yyyy-MM-dd")}";
            var result = await _webClient.GetAsync($"/v2/parcel_shops/around/schedule{urlParams}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<ParcelShopsResponse>().ConfigureAwait(false);
            else
            {
                var error = result.Content.ReadAsAsync<ErrorResponce>().Result;
                throw new HttpRequestException($"Getting parcel shops failed with message: {error.Message}");
            }
        }

        public async Task<ZoneResponse> GetZoneCoverage(string city, RecivingType recivingType = RecivingType.picking)
        {
            var urlParams = $"{city}?type={Enum.GetName(typeof(RecivingType), recivingType)}";
            var result = await _webClient.GetAsync($"/v2/areas/{urlParams}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<ZoneResponse>().ConfigureAwait(false);
            else
            {
                var error = result.Content.ReadAsAsync<ErrorResponce>().Result;
                throw new HttpRequestException($"Getting zone coverage failed with message: {error.Error}");
            }
        }

        public async Task<bool> Validate(string address, RecivingType recivingType, string phone = "")
        {
            var urlParams = $"?type={Enum.GetName(typeof(RecivingType), recivingType)}&address={WebUtility.UrlEncode(address)}";
            urlParams += phone != string.Empty ? $"&phone={phone}" : string.Empty;
            var result = await _webClient.GetAsync($"/v2/addresses/validate{urlParams}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                return (bool)response.FirstOrDefault(x => x.Key == "success").Value;
            }
            else
            {
                var error = result.Content.ReadAsAsync<ErrorResponce>().Result;
                throw new HttpRequestException($"Address validation failed with message: {error.Message}");
            }
        }
    }
}
