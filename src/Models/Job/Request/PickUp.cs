using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class PickUp
    {
        /// <summary>
        /// String that fully identifies the picking address.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        [JsonRequired]
        public string Address { get; set; }

        /// <summary>
        /// A comment for the courier to help him pickup the package; floor number, door, etc.
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Contact information at the origin.
        /// </summary>
        [JsonProperty(PropertyName = "contact")]
        public Contact Contact { get; set; }

        [JsonProperty(PropertyName = "access_codes")]
        public IEnumerable<AccessCode> AccessCodes { get; set; }
    }
}