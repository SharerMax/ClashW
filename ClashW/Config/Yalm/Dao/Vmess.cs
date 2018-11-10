using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ClashW.Config.Yaml.Dao
{
    partial class Proxy
    {
        [YamlMember(Alias = "uuid", ApplyNamingConventions = false)]
        public string Uuid { get; set; }
        [YamlMember(Alias = "alterId", ApplyNamingConventions = false)]
        public string AlterId { get; set; }
        [YamlMember(Alias = "network", ApplyNamingConventions = false)]
        public string Network { get; set; }
        [YamlMember(Alias = "ws-path", ApplyNamingConventions = false)]
        public string WsPath { get; set; }
    }
}
