using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClashW.Config.Api.Dao
{
    
    public class LogMessage
    {
        [JsonProperty(PropertyName="type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; set; }
    }
}
