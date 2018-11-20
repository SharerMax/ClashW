using ClashW.Config.Yaml.Dao;
using ClashW.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Config
{
    public sealed class ConfigHelper
    {
        public  static void GenerateProxyGroup(YamlConfig yamlConfig, string testUrl)
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
            selectProxyGroup.Name = AppContract.ReservedKey.PROXY_POLICY_SELECT_NAME;
            selectProxyGroup.Type = "select";
            selectProxyGroup.Proxies = new List<string>();
            var autoProxyGroup = new ProxyGroup();
            autoProxyGroup.Name = AppContract.ReservedKey.PROXY_POLICY_URL_TEST_NAME;
            autoProxyGroup.Type = "url-test";
            autoProxyGroup.Url = testUrl;
            autoProxyGroup.Interval = 500;
            autoProxyGroup.Proxies = new List<string>();
            var fallbackAutoGroup = new ProxyGroup();
            fallbackAutoGroup.Name = AppContract.ReservedKey.PROXY_POLICY_FALLBACK_NAME;
            fallbackAutoGroup.Type = "fallback";
            fallbackAutoGroup.Url = testUrl;
            fallbackAutoGroup.Interval = 500;
            fallbackAutoGroup.Proxies = new List<string>();

            foreach (Proxy proxy in yamlConfig.ProxyList)
            {
                var proxyName = proxy.Name;
                selectProxyGroup.Proxies.Add(proxyName);
                autoProxyGroup.Proxies.Add(proxyName);
                fallbackAutoGroup.Proxies.Add(proxyName);
            }
            selectProxyGroup.Proxies.Add(AppContract.ReservedKey.PROXY_POLICY_URL_TEST_NAME);
            selectProxyGroup.Proxies.Add(AppContract.ReservedKey.PROXY_POLICY_FALLBACK_NAME);
            yamlConfig.ProxyGroups.Add(autoProxyGroup);
            yamlConfig.ProxyGroups.Add(fallbackAutoGroup);
            yamlConfig.ProxyGroups.Add(selectProxyGroup);
        }
    }
}
