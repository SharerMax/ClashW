using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashW.Config.Api;
using ClashW.Config.Yaml;
using ClashW.Config.Yaml.Dao;
using ClashW.ProcessManager;
using ClashW.Utils;
using ClashW.View;
using Newtonsoft.Json;
using static ClashW.Config.Api.ClashApi;

namespace ClashW.Config
{
    public enum RunningMode { DIRECT, GLOBAL, RULE };
    public enum LogLevel { INFO, WARNING, ERROR, DEBUG};
    public sealed class ConfigController
    {
        private static ConfigController controller;
        private static object _lock = new object();
       

        #region clash 运行模式定义
        private const string RUNNING_MODE_RULE = "Rule";
        private const string RUNNING_MODE_DIRECT = "Direct";
        private const string RUNNING_MODE_GLOBAL = "Global";
        #endregion

        public const string LOG_LEVEL_INFO = "info";
        public const string LOG_LEVEL_WARING = "warning";
        public const string LOG_LEVEL_ERROR = "error";
        public const string LOG_LEVEL_DEBUG = "debug";

        private YamlConfig yamlConfig;
        private ClashProcessManager clashProcessManager;

        //public delegate void TrafficChangedHandler(ConfigController configController);
        //public event TrafficChangedHandler TrafficChangedEvent;

        public delegate void ProxyListChangedHandler(ConfigController configController, List<Proxy> proxyList);
        public event ProxyListChangedHandler ProxyChangedEvent;

        public delegate void RunningModeChangedHandler(ConfigController configController, RunningMode runningMode);
        public event RunningModeChangedHandler RunningModeChangedEvent;

        public delegate void SystemProxyChangedHandler(ConfigController configController, string proxyHost, bool enable);
        public event SystemProxyChangedHandler SystemProxyChangedEvent;

        public delegate void SelectedProxyChangedHandler(ConfigController configController, Proxy proxy);
        public event SelectedProxyChangedHandler SelectedProxyChangedEvent;

        private ClashApi clashApi;

        private const string DEFAULT_PROXY_HOST = "127.0.0.1:7891";
        public static ConfigController Instance
        {
            get
            {
                if(controller == null)
                {
                    lock(_lock) {
                        if(controller == null)
                        {
                            controller = new ConfigController();
                        }
                    }
                }
                return controller;
            }
        }
        private ConfigController()
        {

        }

        public void Init(ClashProcessManager clashProcessManager)
        {
            this.clashProcessManager = clashProcessManager;
            this.clashProcessManager.ProcessRestartEvent += new ClashProcessManager.ProcessRestartHandler(processRestarted);
            if (yamlConfig == null)
            {
                yamlConfig = YalmConfigManager.Instance.GetYamlConfig();
                YalmConfigManager.Instance.SavedYamlConfigChangedEvent += new YalmConfigManager.SavedYamlConfigChanged(savedYamlConfigChanged);
            }
            clashApi = new ClashApi($"http://{yamlConfig.ExternalController}");
            initClashWConfig();
        }

        private void savedYamlConfigChanged(YalmConfigManager yalmConfigManager, YamlConfig yamlConfig)
        {
            this.yamlConfig = yamlConfig;
            initClashWConfig();
        }

        private void processRestarted(ClashProcessManager processManager)
        {
            initClashWConfig();
        }

        private void initClashWConfig()
        {
            var currentExternalController = yamlConfig.ExternalController;
            if (!yamlConfig.ExternalController.Equals(currentExternalController))
            {
                clashApi.StopLoadLogMessage();
                clashApi.BaseUrl = $"http://{yamlConfig.ExternalController}";
            }

            if (!String.IsNullOrEmpty(Properties.Settings.Default.SelectedServerName))
            {
                foreach (Proxy proxy in yamlConfig.ProxyList)
                {
                    if (Properties.Settings.Default.SelectedServerName.Equals(proxy.Name))
                    {
                        SelecteProxy(proxy);
                    }
                }
            }
            EnableSystemProxy(Properties.Settings.Default.EnableSystemProxy);
            EnableAutoStartup(Properties.Settings.Default.AutoStratup);
        }

        public List<Proxy> AddProxy(Proxy proxy)
        {
            yamlConfig.ProxyList.Add(proxy);
            ConfigHelper.GenerateProxyGroup(yamlConfig);
            saveYamlConfigFile();
            List<Proxy> newProxyList = new List<Proxy>(yamlConfig.ProxyList);
            ProxyChangedEvent?.Invoke(Instance, newProxyList);
            return newProxyList;
        }

        public List<Proxy> RemoveProxy(Proxy proxy)
        {
            yamlConfig.ProxyList.Remove(proxy);
            ConfigHelper.GenerateProxyGroup(yamlConfig);
            saveYamlConfigFile();
            List<Proxy> newProxyList = new List<Proxy>(yamlConfig.ProxyList);
            ProxyChangedEvent?.Invoke(Instance, newProxyList);
            return new List<Proxy>(yamlConfig.ProxyList);
        }

        public List<Proxy> GetProxyList()
        {
            if (yamlConfig.ProxyList == null)
            {
                yamlConfig.ProxyList = new List<Proxy>();
            }
            return new List<Proxy>(yamlConfig.ProxyList);
        }

        public List<string> GetSeletableProxyName()
        {
            if (yamlConfig.ProxyGroups != null)
            {
                foreach(var proxyGroup in yamlConfig.ProxyGroups)
                {
                    if (proxyGroup.Type.Equals("select"))
                    {
                        return new List<string>(proxyGroup.Proxies);
                    }
                }
            }
            return null;
        }

        public void SelecteProxy(Proxy proxy)
        {
            clashApi.SelectedProxy(proxy.Name);
            Properties.Settings.Default.SelectedServerName = proxy.Name;
            Properties.Settings.Default.Save();
        }

        public void SelecteProxyByName(string name)
        {
            clashApi.SelectedProxy(name);
            SelectedProxyChangedEvent?.Invoke(Instance, GetSelectedProxy());
            Properties.Settings.Default.SelectedServerName = name;
            Properties.Settings.Default.Save();
        }

        public Proxy GetSelectedProxy()
        {
            var content = clashApi.ProxyInfo("Proxy");
            Dictionary<string, object> valuse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            if(valuse !=null && valuse.ContainsKey("now"))
            {
                var proxyName = valuse["now"];
                foreach(Proxy proxy in yamlConfig.ProxyList)
                {
                    if (proxyName.Equals(proxy.Name))
                    {
                        return proxy;
                    }
                }
            }
            return null;
        }

        public void RequestProxyDelay(Proxy proxy, ProxyDelayHandler proxyDelayHandler)
        {
            RequestProxyDelayByName(proxy.Name, proxyDelayHandler);
        }
        public void RequestProxyDelayByName(string name, ProxyDelayHandler proxyDelayHandler)
        {
            clashApi.ProxyDelay(name, 3000, "https://www.bing.com", proxyDelayHandler);
        }

        public void EnableSystemProxy(bool enable)
        {
            var proxyhost = yamlConfig.Port == 0 ? DEFAULT_PROXY_HOST : $"127.0.0.1:{yamlConfig.Port}";
            var systemProxyEnable = ProxyUtils.ProxyEnabled(proxyhost);
            if (enable && !systemProxyEnable)
            {
                ProxyUtils.SetProxy(proxyhost, true);
                SystemProxyChangedEvent?.Invoke(Instance, proxyhost, enable);
            }
            
            if(!enable && systemProxyEnable)
            {
                ProxyUtils.SetProxy("", false);
                SystemProxyChangedEvent?.Invoke(Instance, proxyhost, enable);
            }
            Properties.Settings.Default.EnableSystemProxy = enable;
            Properties.Settings.Default.Save();
        }

        public void EnableAutoStartup(bool enable)
        {
            if((enable && !Properties.Settings.Default.AutoStratup) || (!enable && Properties.Settings.Default.AutoStratup))
            {
                StartupUtils.EnableAutoStartup(enable);
                Properties.Settings.Default.AutoStratup = enable;
                Properties.Settings.Default.Save();
            }
        }

        public bool CheckAutoStartupEnable()
        {
            return Properties.Settings.Default.AutoStratup;
        }

        public bool CheckSystemProxyEnable()
        {
            var proxyhost = yamlConfig.Port == 0 ? DEFAULT_PROXY_HOST : $"127.0.0.1:{yamlConfig.Port}";
            return ProxyUtils.ProxyEnabled(proxyhost);
        }

        public void SwitchRunningMode(RunningMode mode)
        {
            if(GetRunningMode() == mode)
            {
                return;
            }

            switch (mode)
            {
                case RunningMode.DIRECT:
                    yamlConfig.Mode = RUNNING_MODE_DIRECT;
                    break;
                case RunningMode.GLOBAL:
                    yamlConfig.Mode = RUNNING_MODE_GLOBAL;
                    break;
                case RunningMode.RULE:
                    yamlConfig.Mode = RUNNING_MODE_RULE;
                    break;
                default:
                    break;
            }
            clashApi.SwitchMode(yamlConfig.Mode);
            saveYamlConfigFile(false);
            RunningModeChangedEvent?.Invoke(Instance, GetRunningMode());
        }

        public RunningMode GetRunningMode()
        {
            RunningMode runningMode = RunningMode.DIRECT;
            switch (yamlConfig.Mode)
            {
                case RUNNING_MODE_RULE:
                    runningMode = RunningMode.RULE;
                    break;
                case RUNNING_MODE_DIRECT:
                    runningMode = RunningMode.DIRECT;
                    break;
                case RUNNING_MODE_GLOBAL:
                    runningMode = RunningMode.GLOBAL;
                    break;
                default:
                    break;
            }
            return runningMode;
        }

        public void StartLoadMessage(LogMessageHandler logMessageHandler)
        {
            clashApi.LogMessageOutputEvent += logMessageHandler;
            clashApi.StartLoadLogMessage();
        }

        public void StopLoadMessage(LogMessageHandler logMessageHandler)
        {
            clashApi.LogMessageOutputEvent -= logMessageHandler;
            clashApi.StopLoadLogMessage();
        }

        public void StartLoadTrafficInfo(TrafficInfoHandler trafficChangedHandler)
        {
            clashApi.TrafficInfoEvent += trafficChangedHandler;
            clashApi.StartLoadTrafficInfo();
        }

        public void StopLoadTrafficInfo(TrafficInfoHandler trafficChangedHandler)
        {
            clashApi.TrafficInfoEvent -= trafficChangedHandler;
            clashApi.StopLoadTrafficInfo();
        }

        public int GetListenedSocksPort()
        {
            return yamlConfig.SocksPort;
        }

        public int GetListenedHttpProt()
        {
            return yamlConfig.Port;
        }

        public bool IsAllowLan()
        {
            return yamlConfig.AllowLan;
        }

        public string GetExternalController()
        {
            return yamlConfig.ExternalController;
        }

        public string GetExternalControllerSecret()
        {
            return yamlConfig.Secret;
        }

        public LogLevel GetLogLevel()
        {
            LogLevel logLevel;
            switch(yamlConfig.LogLevel)
            {
                case LOG_LEVEL_INFO:
                    logLevel = LogLevel.INFO;
                    break;
                case LOG_LEVEL_WARING:
                    logLevel = LogLevel.WARNING;
                    break;
                case LOG_LEVEL_ERROR:
                    logLevel = LogLevel.ERROR;
                    break;
                case LOG_LEVEL_DEBUG:
                    logLevel = LogLevel.DEBUG;
                    break;
                default:
                    logLevel = LogLevel.INFO;
                    break;
            }
            return logLevel;
        }

        public static ConfigEditor GetConfigEditor()
        {
            return new ConfigEditor();
        }

        private void saveYamlConfigFile()
        {

            saveYamlConfigFile(true);
        }

        private void saveYamlConfigFile(bool restart)
        {
            YalmConfigManager.Instance.SaveYamlConfigFile(yamlConfig);
            if(restart)
            {
                clashProcessManager.Restart();
            }
        }

        public static void EnsureRunningConfig()
        {
            YalmConfigManager.Instance.EnsureYamlConfig();
        }

        public static bool CheckYamlConfigFileExists()
        {
           return YalmConfigManager.CheckYamlConfigFileExists();
        }
    }
}
