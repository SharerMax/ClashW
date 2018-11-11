using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClashW.Config.Api.Dao
{
    public class TrafficInfo
    {
        [JsonProperty(PropertyName = "up")]
        public long Up { get; set; }
        [JsonProperty(PropertyName = "down")]
        public long Down { get; set; }
    }
}
