using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ClashW.Config.Api;
using ClashW.Config.FileSystemWather;
using ClashW.Config.Yaml;
using ClashW.Config.Yaml.Dao;
using ClashW.Log;
using ClashW.ProcessManager;
using ClashW.Utils;
using ClashW.View;
using Newtonsoft.Json;
using RestSharp;
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
        private Timer onlineRuleUpdateTimer;

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

        public delegate void PortErrorHandler(ConfigController configController, PortErrorEventArgs portErrorEventArgs);
        public event PortErrorHandler PortErrorEvent;

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
            if (yamlConfig == null)
            {
                YalmConfigManager.Instance.SavedYamlConfigChangedEvent += new YalmConfigManager.SavedYamlConfigChanged(savedYamlConfigChanged);
                yamlConfig = YalmConfigManager.Instance.GetYamlConfig();  
            }
        }

        public bool Start(ClashProcessManager clashProcessManager)
        {
            this.clashProcessManager = clashProcessManager;
            if (checkReadyRunConfig())
            {
                clashProcessManager.Start();
                clashApi = new ClashApi($"http://{yamlConfig.ExternalController}");

                UserRuleFileSystemWatcher.Instance.Start();
                if (!String.IsNullOrEmpty(Properties.Settings.Default.OnlineRuleUrl))
                {
                    startOnlineRuleUpdateTimer();
                }
                initClashWConfig();
                return true;
            }
            return false;
        }

        private bool checkReadyRunConfig()
        {
            int httpPort = yamlConfig.Port;
            int socksPort = yamlConfig.SocksPort;
            int externalPort = Convert.ToInt32(yamlConfig.ExternalController.Split(':')[1]);
            if (PortUtils.TcpPortIsUsed(httpPort))
            {
                PortErrorEvent?.Invoke(this, new PortErrorEventArgs(httpPort, PortErrorEventArgs.HTTP_PORT_ERROR));
                return false;
            }
            if (PortUtils.TcpPortIsUsed(socksPort))
            {
                PortErrorEvent?.Invoke(this, new PortErrorEventArgs(socksPort, PortErrorEventArgs.SOCKS_PORT_ERROR));
                return false;
            }
            if (PortUtils.TcpPortIsUsed(externalPort))
            {
                PortErrorEvent?.Invoke(this, new PortErrorEventArgs(externalPort, PortErrorEventArgs.EXTERNAL_CONTROL_PORT));
                return false;
            }
            return true;
        }

        private void startOnlineRuleUpdateTimer()
        {
            if(onlineRuleUpdateTimer == null)
            {
                onlineRuleUpdateTimer = new Timer();
            }
            
            onlineRuleUpdateTimer.BeginInit();
            onlineRuleUpdateTimer.AutoReset = true;
            DateTime preUpdateDateTime = Properties.Settings.Default.OnlineUpdateTime;
            var interval = (DateTime.Now.ToFileTimeUtc() - preUpdateDateTime.ToFileTimeUtc()) / 1000;
            if(interval >= Properties.Settings.Default.OnlineUpdateIntervalHour * 60 * 60 * 1000)
            {
                interval = 1;
            }
            else
            {
                interval = Properties.Settings.Default.OnlineUpdateIntervalHour * 60 * 60 * 1000 - interval;
            }
            onlineRuleUpdateTimer.Interval = interval;
            onlineRuleUpdateTimer.Elapsed += new ElapsedEventHandler(onlineRuleTimeElapsedHandler);
            onlineRuleUpdateTimer.EndInit();
            if(!onlineRuleUpdateTimer.Enabled)
            {
                onlineRuleUpdateTimer.Start();
            }
        }

        private void stopOnlineRuleUpdateTimer()
        {
            if(onlineRuleUpdateTimer != null)
            {
                onlineRuleUpdateTimer.Stop();
                onlineRuleUpdateTimer.Dispose();
                onlineRuleUpdateTimer = null;
            }
        }

        private void onlineRuleTimeElapsedHandler(object sender, ElapsedEventArgs e)
        {
            Loger.Instance.Write("开始更新在线规则");
            onlineRuleUpdateTimer.Interval = Properties.Settings.Default.OnlineUpdateIntervalHour * 60 * 60 * 1000;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.OnlineRuleUrl))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(onLineRuleDownloadComplete);
                if(!Directory.Exists(AppContract.Path.RULE_DIR))
                {
                    Directory.CreateDirectory(AppContract.Path.RULE_DIR);
                }
                webClient.DownloadFileAsync(new Uri(Properties.Settings.Default.OnlineRuleUrl), AppContract.Path.ONLINE_RULE_PATH);
            }
           
        }

        private void onLineRuleDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                Loger.Instance.Write(e.Error);
                return;
            }

            if(e.Cancelled)
            {
                Loger.Instance.Write("取消在线更新规则任务");
                return;
            }
            Properties.Settings.Default.OnlineUpdateTime = DateTime.Now;
            YalmConfigManager.Instance.UpdateRuleConfig();
            Loger.Instance.Write("在线规则更新结束");

        }

        private void savedYamlConfigChanged(YalmConfigManager yalmConfigManager, YamlConfig yamlConfig)
        {
            this.yamlConfig = yamlConfig;
            if(clashProcessManager.IsRunning)
            {
                initClashWConfig();
            }
            
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
            ConfigHelper.GenerateProxyGroup(yamlConfig, Properties.Settings.Default.TestUrl);
            saveYamlConfigFile();
            List<Proxy> newProxyList = new List<Proxy>(yamlConfig.ProxyList);
            ProxyChangedEvent?.Invoke(Instance, newProxyList);
            return newProxyList;
        }

        public List<Proxy> RemoveProxy(Proxy proxy)
        {
            yamlConfig.ProxyList.Remove(proxy);
            ConfigHelper.GenerateProxyGroup(yamlConfig, Properties.Settings.Default.TestUrl);
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
            clashApi.ProxyDelay(name, 3000, Properties.Settings.Default.TestUrl, proxyDelayHandler);
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

        public void ReLoadRule()
        {
            if(YalmConfigManager.Instance.UpdateRuleConfig())
            {
                clashApi.ReLoadRule();
            }
        }

        public void SetConfig(int port, int socksPort, bool allowLan, string mode, string logLevel)
        {
            if(clashProcessManager.IsRunning)
            {
                clashApi.Config(port, socksPort, allowLan, mode, logLevel);
            }
        }

        public void SetOnlineRuleUrl(string onlineRuleUrl, int cycleHour)
        {
            var savedUrl = Properties.Settings.Default.OnlineRuleUrl;
            if (String.IsNullOrEmpty(savedUrl) && !String.IsNullOrEmpty(onlineRuleUrl))
            {
                Properties.Settings.Default.OnlineUpdateTime = new DateTime(2018, 1, 1);
                stopOnlineRuleUpdateTimer();
                startOnlineRuleUpdateTimer();
                YalmConfigManager.Instance.UpdateRuleConfig();
            }

            if(String.IsNullOrEmpty(onlineRuleUrl))
            {
                stopOnlineRuleUpdateTimer();
                Loger.Instance.Write("删除在线规则");
                YalmConfigManager.Instance.ClearOnlineRule();
                Properties.Settings.Default.OnlineUpdateTime = new DateTime(2018, 1, 1);
            }

            Properties.Settings.Default.OnlineRuleUrl = onlineRuleUrl;
            Properties.Settings.Default.OnlineUpdateIntervalHour = cycleHour;
            Properties.Settings.Default.Save();
        }

        public string GetOnlineRuleUrl()
        {
            return Properties.Settings.Default.OnlineRuleUrl;
        }

        public int GetOnlineRuleUpdateCycleHour()
        {
            return Properties.Settings.Default.OnlineUpdateIntervalHour;
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
