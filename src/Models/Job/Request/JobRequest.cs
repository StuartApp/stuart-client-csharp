using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StuartDelivery.Models.Job.Request
{
    public class JobRequest
    {
        [JsonProperty(PropertyName = "job")]
        public Job Job { get; set; }
    }
}
