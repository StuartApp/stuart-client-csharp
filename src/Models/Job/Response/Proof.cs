using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Proof
    {
        /// <summary>
        /// The proof of delivery, signature made by the package recipient.
        /// </summary>
        [JsonProperty(PropertyName = "signature_url")]
        public string SignatureUrl { get; set; }
    }
}