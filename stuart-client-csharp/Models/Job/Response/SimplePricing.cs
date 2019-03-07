using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class SimplePricing
    {
        /// <summary>
        /// Price VAT excluded
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        [JsonRequired]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        [JsonRequired]
        public string Currency { get; set; }
    }
}
