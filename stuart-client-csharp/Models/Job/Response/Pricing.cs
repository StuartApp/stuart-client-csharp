using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Pricing
    {
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "tax_percentage")]
        public double TaxPercentage { get; set; }

        [JsonProperty(PropertyName = "price_tax_included")]
        public double PriceTaxIncluded { get; set; }

        [JsonProperty(PropertyName = "price_tax_excluded")]
        public double PriceTaxExcluded { get; set; }

        [JsonProperty(PropertyName = "tax_amount")]
        public double TaxAmount { get; set; }

        [JsonProperty(PropertyName = "invoice_url")]
        public string InvoiceUrl { get; set; }
    }
}