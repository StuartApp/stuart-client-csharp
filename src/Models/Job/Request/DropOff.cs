using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StuartDelivery.Models.Job.Enums;

namespace StuartDelivery.Models.Job.Request
{
    public class DropOff
    {
        //Which package size you want to send. Mandatory if transport_type is empty. 🇪🇸 🇬🇧 Spain and UK only
        [JsonProperty(PropertyName = "package_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageSizeType PackageType { get; set; }

        //A string that allows you to give more information on the package itself so it can be identified better.
        [JsonProperty(PropertyName = "package_description")]
        public string PackageDescription { get; set; }

        //Unique client order reference number.
        [JsonProperty(PropertyName = "client_reference")]
        public string ClientReference { get; set; }

        //String that fully identifies the destination address.
        [JsonProperty(PropertyName = "address")]
        [JsonRequired]
        public string Address { get; set; }

        //A comment for the courier to help him deliver the package; floor number, door, etc.
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        //Contact information at the destination.
        [JsonProperty(PropertyName = "contact")]
        public Contact Contact { get; set; }

        [JsonProperty(PropertyName = "access_codes")]
        public IEnumerable<AccessCode> AccessCodes { get; set; }
    }
}