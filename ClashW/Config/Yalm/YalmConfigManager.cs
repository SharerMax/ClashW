using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using ClashW.Config.Yaml.Dao;

namespace ClashW.Config.Yaml
{
    public sealed class YalmConfigManager
    {
        private static YalmConfigManager instance = null;
        private static readonly object padlock = new object();
        private const string CONFIG_FILE_PATH = @"./config.yml";

        public delegate void SavedYamlConfigChanged(YalmConfigManager sender, YamlConfig yamlConfig);
        public event SavedYamlConfigChanged SavedYamlConfigChangedEvent;


        private YalmConfigManager()
        {

        }
        public static YalmConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new YalmConfigManager();
                        }
                    }
                }
                return instance;
            }
        }

        public YamlConfig GetYamlConfig()
        {
            YamlConfig yamlConfig;
            if (File.Exists(CONFIG_FILE_PATH))
            {
                using (var yarmConfigFileStream = File.Open(CONFIG_FILE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var yarmConfigFileStreamReader = new StreamReader(yarmConfigFileStream);
                    var deserializer = new DeserializerBuilder().Build();
                    yamlConfig = deserializer.Deserialize<YamlConfig>(yarmConfigFileStreamReader);
                }
            }
            else
            {
                yamlConfig = getDefaultYamlConfig();
                SaveYamlConfigFile(yamlConfig);
            }
            return yamlConfig;

        }

        public void EnsureYamlConfig()
        {
            if (!File.Exists(CONFIG_FILE_PATH))
            {
                YamlConfig yamlConfig = getDefaultYamlConfig();
                SaveYamlConfigFile(yamlConfig);
            }
        }

        public void SaveYamlConfigFile(YamlConfig yamlConfig)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(yamlConfig);
            using (var yamlConfigFileStream = File.Open(CONFIG_FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                yamlConfigFileStream.SetLength(0);
                var yamlConfigFileStreamWriter = new StreamWriter(yamlConfigFileStream);
                yamlConfigFileStreamWriter.Write(yaml);
                yamlConfigFileStreamWriter.Flush();
                yamlConfigFileStreamWriter.Close();
            }
            SavedYamlConfigChangedEvent?.Invoke(this, yamlConfig);
        }

       

        private YamlConfig getDefaultYamlConfig()
        {
            var yamlConfig = new YamlConfig();
            yamlConfig.Port = 7890;
            yamlConfig.SocksPort = 7891;
            yamlConfig.AllowLan = true;
            yamlConfig.Mode = "Direct"; // Rule / Global/ Direct 
            yamlConfig.LogLevel = "info"; // info / warning / error / debug
            yamlConfig.ExternalController = "127.0.0.1:9090";
            yamlConfig.Secret = "";
            yamlConfig.ProxyList = new List<Proxy>();
            yamlConfig.RuleList = new List<string>();

            yamlConfig.RuleList.Add("DOMAIN-SUFFIX,local,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,127.0.0.0/8,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,172.16.0.0/12,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,192.168.0.0/16,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,10.0.0.0/8,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,17.0.0.0/8,DIRECT");
            yamlConfig.RuleList.Add("IP-CIDR,100.64.0.0/10,DIRECT");

            yamlConfig.RuleList.Add("DOMAIN-KEYWORD,-cn,DIRECT");
            yamlConfig.RuleList.Add("DOMAIN-SUFFIX,cn,DIRECT");
            yamlConfig.RuleList.Add("GEOIP,CN,DIRECT");
            yamlConfig.RuleList.Add("FINAL,,Proxy");
            return yamlConfig;
        }

        public static bool CheckYamlConfigFileExists()
        {
            return File.Exists(CONFIG_FILE_PATH);
        }
    }

    
}