﻿using ClashW.Config.Yaml;
using ClashW.Config.Yaml.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Config
{
    public sealed class ConfigEditor
    {
        private YamlConfig yamlConfig;
        private string testUrl;
        public ConfigEditor()
        {
            yamlConfig = YalmConfigManager.Instance.GetYamlConfig();
            testUrl = Properties.Settings.Default.TestUrl;
            YalmConfigManager.Instance.SavedYamlConfigChangedEvent += new YalmConfigManager.SavedYamlConfigChanged(savedYamlConfig);
        }

        public void SetListenedHttpPort(int httpPort)
        {
            yamlConfig.Port = httpPort;
        }

        public void SetListenedSocksPort(int socksPort)
        {
            yamlConfig.SocksPort = socksPort;
        }

        public void AllowLan(bool allow)
        {
            yamlConfig.AllowLan = allow;
        }

        public void SetExternalController(string externalController)
        {
            yamlConfig.ExternalController = externalController;
        }

        public void SetExteranlControllerSecret(string secret)
        {
            yamlConfig.Secret = secret;
        }

        public void AddProxy(Proxy proxy)
        {
            yamlConfig.ProxyList.Add(proxy);
            ConfigHelper.GenerateProxyGroup(yamlConfig, Properties.Settings.Default.TestUrl);
        }

        public void AddProxy(int index, Proxy proxy)
        {
            if(index < 0)
            {
                throw new ArgumentException("index 不能小于0");
            }

            if(index > yamlConfig.ProxyList.Count)
            {
                throw new ArgumentOutOfRangeException($"index 超出范围，最大长度为{yamlConfig.ProxyList.Count}");
            }

            yamlConfig.ProxyList.Insert(index, proxy);
        }

        public void RemoveProxy(Proxy proxy)
        {
            yamlConfig.ProxyList.Remove(proxy);
            ConfigHelper.GenerateProxyGroup(yamlConfig, Properties.Settings.Default.TestUrl);
        }

        public void RemoveProxyByName(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return;
            }
            Proxy deletedProxy = null;
            foreach(Proxy proxy in yamlConfig.ProxyList)
            {
                if(name.Equals(proxy.Name))
                {
                    deletedProxy = proxy;
                    break;
                }
            }
            RemoveProxy(deletedProxy);
        }

        public void SetLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.INFO:
                    yamlConfig.LogLevel = "info";
                    break;
                case LogLevel.WARNING:
                    yamlConfig.LogLevel = "warnig";
                    break;
                case LogLevel.ERROR:
                    yamlConfig.LogLevel = "error";
                    break;
                case LogLevel.DEBUG:
                    yamlConfig.LogLevel = "debug";
                    break;
                default:
                    break;
            }
        }

        public void SetTestUrl(string testUrl)
        {
            this.testUrl = testUrl;
        }

        public void Commit()
        {
            if(!testUrl.Equals(Properties.Settings.Default.TestUrl))
            {
                Properties.Settings.Default.TestUrl = testUrl;
                Properties.Settings.Default.Save();
                ConfigHelper.GenerateProxyGroup(yamlConfig, testUrl);
            }
            
            YalmConfigManager.Instance.SaveYamlConfigFile(yamlConfig);
            ProcessManager.ClashProcessManager.Instance.Restart();
        }

        private void savedYamlConfig(YalmConfigManager sender, YamlConfig yamlConfig)
        {
            this.yamlConfig = yamlConfig;
        }
    }
}
