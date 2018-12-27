using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using ClashW.Config.Yaml.Dao;

namespace ClashW.Config.Yaml.Dao
{
    public partial class YamlConfig
    {
        [YamlMember(Alias = "Proxy", ApplyNamingConventions = false)]
        public List<Proxy> ProxyList { get; set; }
        [YamlMember(Alias = "Proxy Group", ApplyNamingConventions = false)]
        public List<ProxyGroup> ProxyGroups { get; set; }
        [YamlMember(Alias = "Rule", ApplyNamingConventions = false)]
        public List<string> RuleList { get; set; }
    }
}
