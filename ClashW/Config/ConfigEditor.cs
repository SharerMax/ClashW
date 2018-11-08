using ClashW.Config.Yaml;
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
        public ConfigEditor()
        {
            yamlConfig = YalmConfigManager.Instance.GetYamlConfig();
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

        public void Commit()
        {
            YalmConfigManager.Instance.SaveYamlConfigFile(yamlConfig);
            ProcessManager.ClashProcessManager.Instance.Restart();
        }
    }
}
