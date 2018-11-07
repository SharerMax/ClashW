using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashW.Config.Yaml.Dao
{
    public partial class YamlConfig
    {
        [YamlMember(Alias = "port", ApplyNamingConventions = false)]
        public int Port { get; set; }
        [YamlMember(Alias = "socks-port", ApplyNamingConventions = false)]
        public int SocksPort { get; set; }
        [YamlMember(Alias = "mode", ApplyNamingConventions = false)]
        public string Mode { get; set; }
        [YamlMember(Alias = "allow-lan", ApplyNamingConventions = false)]
        public bool AllowLan { get; set; }
        [YamlMember(Alias = "log-level", ApplyNamingConventions = false)]
        public string LogLevel { get; set; }
        [YamlMember(Alias = "external-controller", ApplyNamingConventions = false)]
        public string ExternalController { get; set; }
        [YamlMember(Alias = "secret", ApplyNamingConventions = false)]
        public string Secret { get; set; }
        
    }
}
