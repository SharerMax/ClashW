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
using ClashW.Utils;
using System.Security.Cryptography;

namespace ClashW.Config.Yaml
{
    public sealed class YalmConfigManager
    {
        private static YalmConfigManager instance = null;
        private static readonly object padlock = new object();
        private static readonly string CONFIG_FILE_PATH = AppContract.CLASH_CONFIG_PATH;

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
            UpdateRuleConfig();
        }

        public YamlConfig GetUserRuleYamlConfig()
        {
            if(!File.Exists(AppContract.USER_RULE_PATH))
            {
                return null;
            }
            using (var yarmConfigFileStream = File.Open(AppContract.USER_RULE_PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                YamlConfig userRuleYamlConfig;
                var yarmConfigFileStreamReader = new StreamReader(yarmConfigFileStream);
                var deserializer = new DeserializerBuilder().Build();
                userRuleYamlConfig = deserializer.Deserialize<YamlConfig>(yarmConfigFileStreamReader);
                return userRuleYamlConfig;
            }
        }

        public YamlConfig GetOnlineRuleYamlConfig()
        {
            if(!File.Exists(AppContract.ONLINE_RULE_PATH))
            {
                return null;
            }
            using (var yarmConfigFileStream = File.Open(AppContract.ONLINE_RULE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                YamlConfig onlineRuleYamlConfig;
                var yarmConfigFileStreamReader = new StreamReader(yarmConfigFileStream);
                var deserializer = new DeserializerBuilder().Build();
                onlineRuleYamlConfig = deserializer.Deserialize<YamlConfig>(yarmConfigFileStreamReader);
                return onlineRuleYamlConfig;
            }
        }

        public bool UpdateRuleConfig()
        {
            bool needUpdate = false;
            YamlConfig yamlConfig = GetYamlConfig();
            if (UserRuleIsChanged())
            {
                YamlConfig userRule = GetUserRuleYamlConfig();
                
                if (yamlConfig.RuleList != null)
                {
                    yamlConfig.RuleList.Clear();
                    if(userRule != null && userRule.RuleList != null)
                    {
                        yamlConfig.RuleList.AddRange(userRule.RuleList);
                    }
                }
                else
                {
                    yamlConfig.RuleList = userRule == null ? null : userRule.RuleList;
                }
                needUpdate = true;
                Properties.Settings.Default.UserRuleMD5 = MD5Utils.ComputeFileMD5(AppContract.USER_RULE_PATH);
            }

            if(OnlineRuleIsChanged())
            {
                YamlConfig onlineRule = GetOnlineRuleYamlConfig();
                string lastUserRule = yamlConfig.RuleList == null || yamlConfig.RuleList.Count == 0 ? String.Empty : yamlConfig.RuleList.Last();
                string finalUserRule = String.IsNullOrEmpty(lastUserRule) || !lastUserRule.StartsWith("FINAL") ? String.Empty : lastUserRule;
                if(!String.IsNullOrEmpty(finalUserRule))
                {
                    yamlConfig.RuleList.RemoveAt(yamlConfig.RuleList.Count - 1);
                }

                if (yamlConfig.RuleList != null && onlineRule != null && onlineRule.RuleList !=null )
                {
                    yamlConfig.RuleList.AddRange(onlineRule.RuleList);
                }

                if(yamlConfig.RuleList == null)
                {
                    yamlConfig.RuleList = onlineRule == null ? null : onlineRule.RuleList;
                }

                string lastOnlineRule = string.Empty;
                lastOnlineRule = yamlConfig.RuleList == null || yamlConfig.RuleList.Count == 0 ? String.Empty : onlineRule.RuleList.Last();
                string finalOnlineRule = String.IsNullOrEmpty(lastOnlineRule) || !lastOnlineRule.StartsWith("FINAL") ? String.Empty : lastOnlineRule;
                if(!String.IsNullOrEmpty(finalUserRule))
                {
                    if(!String.IsNullOrEmpty(finalOnlineRule))
                    {
                        yamlConfig.RuleList?.RemoveAt(yamlConfig.RuleList.Count - 1);
                    }
                    yamlConfig.RuleList?.Add(finalUserRule);
                }
                needUpdate = true;
                Properties.Settings.Default.UserRuleMD5 = MD5Utils.ComputeFileMD5(AppContract.ONLINE_RULE_PATH);
            }

            if(needUpdate)
            {
                SaveYamlConfigFile(yamlConfig);
                Properties.Settings.Default.Save();
            }
            return needUpdate;
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

        private void saveDefaultUserRule(YamlConfig yamlConfig)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(yamlConfig);
            if(!Directory.Exists(AppContract.RULE_DIR))
            {
                Directory.CreateDirectory(AppContract.RULE_DIR);
            }
            using (var yamlConfigFileStream = File.Open(AppContract.USER_RULE_PATH, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                yamlConfigFileStream.SetLength(0);
                var yamlConfigFileStreamWriter = new StreamWriter(yamlConfigFileStream);
                yamlConfigFileStreamWriter.Write(yaml);
                yamlConfigFileStreamWriter.Flush();
                yamlConfigFileStreamWriter.Close();
            }
            Properties.Settings.Default.UserRuleMD5 = MD5Utils.ComputeFileMD5(AppContract.USER_RULE_PATH);
            Properties.Settings.Default.Save();
        }

        private YamlConfig getDefaultYamlConfig()
        {
            var yamlConfig = new YamlConfig();
            yamlConfig.Port = 7890;
            yamlConfig.SocksPort = 7891;
            yamlConfig.AllowLan = false;
            yamlConfig.Mode = "Direct"; // Rule / Global/ Direct 
            yamlConfig.LogLevel = "info"; // info / warning / error / debug
            yamlConfig.ExternalController = "127.0.0.1:9090";
            yamlConfig.Secret = "";
            yamlConfig.ProxyList = new List<Proxy>();
            yamlConfig.RuleList = generateDefaultUserRule().RuleList;
            return yamlConfig;
        }

        private YamlConfig generateDefaultUserRule()
        {

            var yamlConfig = GetUserRuleYamlConfig();
            if(yamlConfig == null)
            {
                yamlConfig = new YamlConfig();
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
                saveDefaultUserRule(yamlConfig);
            }
            
            return yamlConfig;
        }

        public static bool CheckYamlConfigFileExists()
        {
            return File.Exists(CONFIG_FILE_PATH);
        }

        public static bool UserRuleIsChanged()
        {
            string originMD5 = Properties.Settings.Default.UserRuleMD5;
            return !CompareFileMD5(originMD5, AppContract.USER_RULE_PATH);
        }

        public static bool OnlineRuleIsChanged()
        {
            string originMD5 = Properties.Settings.Default.OnlineRuleMD5;
            return !CompareFileMD5(originMD5, AppContract.ONLINE_RULE_PATH);
        }

        public void ClearOnlineRule()
        {
            if(File.Exists(AppContract.ONLINE_RULE_PATH))
            {
                File.Delete(AppContract.ONLINE_RULE_PATH);
            }
            UpdateRuleConfig();
        }

        public static bool CompareFileMD5(string md5String, string filePath)
        {
            if (String.IsNullOrEmpty(md5String))
            {
                if(!File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            md5String = md5String.ToUpperInvariant();
            return md5String.Equals(MD5Utils.ComputeFileMD5(filePath));
        }
    }

    
}