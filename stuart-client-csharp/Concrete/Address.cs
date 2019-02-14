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
        private readonly WebClient _webClient;

        public Address(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<Result<ParcelShopsResponse>> GetParcelShops(string address, DateTime date)
        {
            try
            {
                var urlParams = $"?address={WebUtility.UrlEncode(address)}&date={date:yyyy-MM-dd}";
                var response = await _webClient.GetAsync($"/v2/parcel_shops/around/schedule{urlParams}").ConfigureAwait(false);
                var result = new Result<ParcelShopsResponse>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<ParcelShopsResponse>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetParcelShops)} failed", e);
            }
        }

        public async Task<Result<ZoneResponse>> GetZoneCoverage(string city,
            ReceivingType receivingType = ReceivingType.picking)
        {
            try
            {
                var urlParams = $"{city}?type={Enum.GetName(typeof(ReceivingType), receivingType)}";
                var response = await _webClient.GetAsync($"/v2/areas/{urlParams}").ConfigureAwait(false);
                var result = new Result<ZoneResponse>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<ZoneResponse>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetZoneCoverage)} failed", e);
            }
        }

        public async Task<Result<bool>> Validate(string address, ReceivingType receivingType, string phone = "")
        {
            try
            {
                var urlParams = $"?type={Enum.GetName(typeof(ReceivingType), receivingType)}&address={WebUtility.UrlEncode(address)}";
                urlParams += phone != string.Empty ? $"&phone={phone}" : string.Empty;
                var response = await _webClient.GetAsync($"/v2/addresses/validate{urlParams}").ConfigureAwait(false);
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
                throw new HttpRequestException($"{nameof(Validate)} failed", e);
            }
        }
    }
}
