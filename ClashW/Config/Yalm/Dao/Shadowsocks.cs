using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashW.Config.Yaml.Dao
{
    partial class Proxy
    {
        [YamlMember(Alias = "password", ApplyNamingConventions = false)]
        public string Password { get; set; }
        [YamlMember(Alias = "obfs", ApplyNamingConventions = false)]
        public string Obfs { get; set; }
        [YamlMember(Alias = "obfs-host", ApplyNamingConventions = false)]
        public string ObfsHost { get; set; }
    }
}
