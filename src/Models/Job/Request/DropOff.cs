using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class DropOff
    {
        /// <summary>
        /// Which package size you want to send. Mandatory if transport_type is empty. 🇪🇸 🇬🇧 Spain and UK only.
        /// </summary>
        [JsonProperty(PropertyName = "package_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageSizeType PackageType { get; set; }

        /// <summary>
        /// A string that allows you to give more information on the package itself so it can be identified better.
        /// </summary>
        [JsonProperty(PropertyName = "package_description")]
        public string PackageDescription { get; set; }

        /// <summary>
        /// Unique client order reference number.
        /// </summary>
        [JsonProperty(PropertyName = "client_reference")]
        public string ClientReference { get; set; }

        /// <summary>
        /// String that fully identifies the destination address.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        [JsonRequired]
        public string Address { get; set; }

        /// <summary>
        /// A comment for the courier to help him deliver the package; floor number, door, etc.
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Contact information at the destination.
        /// </summary>
        [JsonProperty(PropertyName = "contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// Access codes information at the destination.
        /// </summary>
        [JsonProperty(PropertyName = "access_codes")]
        public IEnumerable<AccessCode> AccessCodes { get; set; }
    }
}