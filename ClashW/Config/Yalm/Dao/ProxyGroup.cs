using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ClashW.Config.Yaml.Dao
{
    public class ProxyGroup
    {
        [YamlMember(Alias = "name", ApplyNamingConventions = false)]
        public string Name { get; set; }
        [YamlMember(Alias = "type", ApplyNamingConventions = false)]
        public string Type { get; set; }
        [YamlMember(Alias = "proxies", ApplyNamingConventions = false)]
        public List<string> Proxies { get; set; }
        [YamlMember(Alias = "url", ApplyNamingConventions = false)]
        public string Url { get; set; }
        [YamlMember(Alias = "interval", ApplyNamingConventions = false)]
        public int Interval { get; set; }
    }
}
