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
        [YamlMember(Alias = "tls", ApplyNamingConventions = false)]
        public bool TLS { get; set; }
        [YamlMember(Alias = "skip-cert-verify", ApplyNamingConventions = false)]
        public bool SkipCertVerify { get; set; }
        
    }
}
