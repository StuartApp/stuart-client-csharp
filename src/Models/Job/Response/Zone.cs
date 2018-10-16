using System.Collections.Generic;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Response
{
    public class Zone
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "region_id")]
        public int RegionId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "routes_to_avoid")]
        public IEnumerable<string> RoutesToAvoid { get; set; }

        [JsonProperty(PropertyName = "short_code")]
        public string ShortCode { get; set; }

        [JsonProperty(PropertyName = "ops_mail")]
        public string OpsMail { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
    }
}