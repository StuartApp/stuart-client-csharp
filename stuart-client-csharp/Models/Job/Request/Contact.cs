using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class Contact
    {
        /// <summary>
        /// Mandatory if company if empty.
        /// </summary>
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Mandatory if company if empty.
        /// </summary>
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Mandatory if firstname or lastname are empty.
        /// </summary>
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}