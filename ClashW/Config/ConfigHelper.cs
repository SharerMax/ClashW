using ClashW.Config.Yaml.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Config
{
    public sealed class ConfigHelper
    {
        public  static void GenerateProxyGroup(YamlConfig yamlConfig)
        {
            if (yamlConfig.ProxyGroups == null)
            {
                yamlConfig.ProxyGroups = new List<ProxyGroup>();
            }
            else
            {
                yamlConfig.ProxyGroups.Clear();
            }

            var selectProxyGroup = new ProxyGroup();
            selectProxyGroup.Name = "Proxy";
            selectProxyGroup.Type = "select";
            selectProxyGroup.Proxies = new List<string>();
            var autoProxyGroup = new ProxyGroup();
            autoProxyGroup.Name = "Auto";
            autoProxyGroup.Type = "url-test";
            autoProxyGroup.Url = "https://www.bing.com";
            autoProxyGroup.Interval = 500;
            autoProxyGroup.Proxies = new List<string>();
            var fallbackAutoGroup = new ProxyGroup();
            fallbackAutoGroup.Name = "FallbackAuto";
            fallbackAutoGroup.Type = "fallback";
            fallbackAutoGroup.Url = "https://www.bing.com";
            fallbackAutoGroup.Interval = 500;
            fallbackAutoGroup.Proxies = new List<string>();

            foreach (Proxy proxy in yamlConfig.ProxyList)
            {
                var proxyName = proxy.Name;
                selectProxyGroup.Proxies.Add(proxyName);
                autoProxyGroup.Proxies.Add(proxyName);
                fallbackAutoGroup.Proxies.Add(proxyName);
            }
            selectProxyGroup.Proxies.Add("Auto");
            selectProxyGroup.Proxies.Add("FallbackAuto");
            yamlConfig.ProxyGroups.Add(autoProxyGroup);
            yamlConfig.ProxyGroups.Add(fallbackAutoGroup);
            yamlConfig.ProxyGroups.Add(selectProxyGroup);
        }
    }
}
