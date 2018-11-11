using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ClashW.Config.Yaml.Dao
{
    public partial class Proxy
    {
        [YamlMember(Alias = "name", ApplyNamingConventions = false)]
        public string Name { get; set; }
        [YamlMember(Alias = "type", ApplyNamingConventions = false)]
        public string Type { get; set; }
        [YamlMember(Alias = "server", ApplyNamingConventions = false)]
        public string Server { get; set; }
        [YamlMember(Alias = "port", ApplyNamingConventions = false)]
        public int Port { get; set; }
        [YamlMember(Alias = "cipher", ApplyNamingConventions = false)]
        public string Cipher { get; set; }

    }
}
