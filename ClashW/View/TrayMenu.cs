using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClashW.Properties;
using ClashW.View;
using ClashW.Config;
using ClashW.Utils;
using ClashW.Config.Yaml.Dao;
using System.IO;

namespace ClashW.View
{
    public class TrayMenu
    {
        private NotifyIcon notify;
        private MainForm mainForm;
        private LogForm logForm;
        private GeneralConfigForm generalConfigForm;
        private TrafficForm trafficForm;
        private ProxyDelayForm delayForm;
        private OnlineRuleForm onlineRuleForm;
        private ContextMenu contextMenu;
        private MenuItem autoBootMenuItem;
        private MenuItem directRunningModeMenuItem;
        private MenuItem globalRunningModeMenuItem;
        private MenuItem ruleRunningModeMenuItem;
        private MenuItem systemProxyMenuItem;
        private MenuItem logFormMenuItem;
        private MenuItem currentSelectedProxyMenuItem;
        private MenuItem currentRunningModeMenuItem;
        private MenuItem proxyServerMenuItemGroup;
        private MenuItem generalConfigMenuItem;
        private MenuItem trafficViewMenuItem;
        private MenuItem onlineRuleMenuItem;
        private MenuItem editeRuleMenuItem;
        private MenuItem importFormClipboard;
        private RunningMode runningMode;
        private List<Proxy> proxyList;
        
        public TrayMenu()
        {
            initMenu();
        }
        
        private void initMenu()
        {
            notify = new NotifyIcon();
            notify.Icon = Resources.tray_logo;
            notify.MouseClick += new MouseEventHandler(tray_clicked);
            runningMode = ConfigController.Instance.GetRunningMode();
            proxyList = ConfigController.Instance.GetProxyList();
            autoBootMenuItem = new MenuItem("开机启动", new EventHandler(autoBootItem_clicked));
            if(ConfigController.Instance.CheckAutoStartupEnable())
            {
                autoBootMenuItem.Checked = true;
            }
            systemProxyMenuItem = new MenuItem("系统代理", new EventHandler(systemProxyItem_clicked));
            if (ConfigController.Instance.CheckSystemProxyEnable())
            {
                systemProxyMenuItem.Checked = true;
            }

            directRunningModeMenuItem = new MenuItem("直连", new EventHandler(directRunningModeItem_clicked));
            globalRunningModeMenuItem = new MenuItem("全局", new EventHandler(globalRunningModeItem_clicked));
            ruleRunningModeMenuItem = new MenuItem("规则", new EventHandler(ruleRunningModeItem_clicked));
            switch(runningMode)
            {
                case RunningMode.DIRECT:
                    currentRunningModeMenuItem = directRunningModeMenuItem;
                    directRunningModeMenuItem.Checked = true;
                    break;
                case RunningMode.GLOBAL:
                    currentRunningModeMenuItem = globalRunningModeMenuItem;
                    globalRunningModeMenuItem.Checked = true;
                    break;
                case RunningMode.RULE:
                    currentRunningModeMenuItem = ruleRunningModeMenuItem;
                    ruleRunningModeMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            ConfigController.Instance.RunningModeChangedEvent += new ConfigController.RunningModeChangedHandler(runningModeChanged);

            logFormMenuItem = new MenuItem("日志", new EventHandler(logFormItem_cicked));

            proxyServerMenuItemGroup = creatMenuGroup("服务器", createProxyListMenuItems());
            ConfigController.Instance.ProxyChangedEvent += new ConfigController.ProxyListChangedHandler(proxyListChanged);
            importFormClipboard = new MenuItem("从剪贴板导入", new EventHandler(importFormClipboardMenuitem_clicked));

            generalConfigMenuItem = new MenuItem("通用配置...", new EventHandler(generalConfigMenuItem_clicked));
            trafficViewMenuItem = new MenuItem("流量...", new EventHandler(trafficViewMenuItem_clicked));

            var ruleMenuItemGroup = creatMenuGroup("规则配置", createRuleMenuItems());

            contextMenu = new ContextMenu(new MenuItem[] {
                autoBootMenuItem,
                systemProxyMenuItem,
                new MenuItem("-"),
                directRunningModeMenuItem,
                globalRunningModeMenuItem,
                ruleRunningModeMenuItem,
                new MenuItem("-"),
                proxyServerMenuItemGroup,
                importFormClipboard,
                new MenuItem("-"),
                logFormMenuItem,
                generalConfigMenuItem,
                trafficViewMenuItem,
                ruleMenuItemGroup,
                new MenuItem("-"),
                new MenuItem("退出", new EventHandler(exitMenuItem_clicked))
            });
            notify.ContextMenu = contextMenu;
        }
        private MenuItem creatMenuGroup(string text, MenuItem [] menuItems)
        {
            MenuItem menuItem = new MenuItem(text, menuItems);
            return menuItem;
        }

        private MenuItem[] createProxyListMenuItems()
        {
            var currentProxy = ConfigController.Instance.GetSelectedProxy();
            
            MenuItem[] menuItems = null;
            var selectableProxyNameList = ConfigController.Instance.GetSeletableProxyName();
            if (selectableProxyNameList != null && selectableProxyNameList.Count > 0)
            {
                var proxyCount = selectableProxyNameList.Count;
                // 代理数 + 测速（1）
                menuItems = new MenuItem[selectableProxyNameList.Count + 1];

                for(var i = 0; i < proxyCount; i++)
                {
                    menuItems[i] = new MenuItem(selectableProxyNameList[i], new EventHandler(proxyNodeItem_clicked));
                    menuItems[i].Tag = selectableProxyNameList[i];
                    if(currentProxy !=null && selectableProxyNameList[i].Equals(currentProxy.Name))
                    {
                        menuItems[i].Checked = true;
                        currentSelectedProxyMenuItem = menuItems[i];
                    }
                }
                menuItems[proxyCount] = new MenuItem("延迟测试", new EventHandler(proxyDelayItem_clicked));
            }

            return menuItems;
            
        }

        private MenuItem[] createRuleMenuItems()
        {
            onlineRuleMenuItem = new MenuItem("在线规则...", new EventHandler(onlineRuleMenuItem_clicked));
            editeRuleMenuItem = new MenuItem("编辑规则...", new EventHandler(editeRuleMenuItem_clicked));
            return new MenuItem[] { onlineRuleMenuItem, editeRuleMenuItem };
        }

        internal void Show()
        {
            notify.Visible = true;
        }

        internal void Close()
        {
            notify.Dispose();
        }

        public void ShowMessage(string tipTitle, string tipText)
        {
            notify.BalloonTipTitle = tipTitle;
            notify.BalloonTipText = tipText;
            notify.BalloonTipIcon = ToolTipIcon.Info;
            notify.ShowBalloonTip(200);
        }

        public void ShowErrorMessage(string tipTitle, string tipText)
        {
            notify.BalloonTipTitle = tipTitle;
            notify.BalloonTipText = tipText;
            notify.BalloonTipIcon = ToolTipIcon.Error;
            notify.ShowBalloonTip(200);
        }

        private void tray_clicked(object sender, MouseEventArgs e)
        {
            
            if(e.Button == MouseButtons.Left)
            {
                if (mainForm == null || mainForm.IsDisposed)
                {
                    mainForm = new MainForm();
                }
                mainForm.Show();
                mainForm.Activate();
                return;
            }

            if(e.Button == MouseButtons.Middle)
            {
                showLogForm();
                return;
            }
        }

        private void logFormItem_cicked(object sender, EventArgs e)
        {
            showLogForm();
        }

        private void showLogForm()
        {
            if (logForm == null || logForm.IsDisposed)
            {
                logForm = new LogForm();
            }

            logForm.Show();
            logForm.Activate();
        }

        private void systemProxyItem_clicked(object sender, EventArgs e)
        {
            if(systemProxyMenuItem.Checked)
            {
                ConfigController.Instance.EnableSystemProxy(false);
                systemProxyMenuItem.Checked = false;
            }
            else
            {
                ConfigController.Instance.EnableSystemProxy(true);
                systemProxyMenuItem.Checked = true;
            }
        }

        private void autoBootItem_clicked(object sender, EventArgs e)
        {
            if(autoBootMenuItem.Checked)
            {
                ConfigController.Instance.EnableAutoStartup(false);
                autoBootMenuItem.Checked = false;
            }
            else
            {
                ConfigController.Instance.EnableAutoStartup(true);
                autoBootMenuItem.Checked = true;
            }
        }

        private void generalConfigMenuItem_clicked(object sender, EventArgs e)
        {
            showGeneralConfigForm();
        }

        private void showGeneralConfigForm()
        {
            if(generalConfigForm == null || generalConfigForm.IsDisposed)
            {
                generalConfigForm = new GeneralConfigForm();
            }

            generalConfigForm.Show();
            generalConfigForm.Activate();
        }

        private void trafficViewMenuItem_clicked(object sender, EventArgs e)
        {
            if(trafficForm == null || trafficForm.IsDisposed)
            {
                trafficForm = new TrafficForm();
            }
            trafficForm.Show();
            trafficForm.Activate();
        }

        private void showTrafficViewForm()
        {

        }

        private void exitMenuItem_clicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void directRunningModeItem_clicked(object sender, EventArgs e)
        {
            currentRunningModeMenuItem.Checked = false;
            directRunningModeMenuItem.Checked = true;
            currentRunningModeMenuItem = directRunningModeMenuItem;
            ConfigController.Instance.SwitchRunningMode(RunningMode.DIRECT);
        }

        private void globalRunningModeItem_clicked(object sender, EventArgs e)
        {
            currentRunningModeMenuItem.Checked = false;
            globalRunningModeMenuItem.Checked = true;
            currentRunningModeMenuItem = globalRunningModeMenuItem;
            ConfigController.Instance.SwitchRunningMode(RunningMode.GLOBAL);
        }

        private void ruleRunningModeItem_clicked(object sender, EventArgs e)
        {
            currentRunningModeMenuItem.Checked = false;
            ruleRunningModeMenuItem.Checked = true;
            currentRunningModeMenuItem = ruleRunningModeMenuItem;
            ConfigController.Instance.SwitchRunningMode(RunningMode.RULE);
        }

        private void runningModeChanged(ConfigController configController, RunningMode runningMode)
        {
            currentRunningModeMenuItem.Checked = false;
            switch (runningMode)
            {
                case RunningMode.DIRECT:
                    directRunningModeMenuItem.Checked = true;
                    currentRunningModeMenuItem = directRunningModeMenuItem;
                    break;
                case RunningMode.GLOBAL:
                    globalRunningModeMenuItem.Checked = true;
                    currentRunningModeMenuItem = globalRunningModeMenuItem;
                    break;
                case RunningMode.RULE:
                    ruleRunningModeMenuItem.Checked = true;
                    currentRunningModeMenuItem = ruleRunningModeMenuItem;
                    break;
                default:
                    break;
            }
        }

        private void proxyListChanged(ConfigController configController, List<Proxy> proxyList)
        {
            this.proxyList = proxyList;
            proxyServerMenuItemGroup.MenuItems.Clear();
            proxyServerMenuItemGroup.MenuItems.AddRange(createProxyListMenuItems());
        }

        private void proxyNodeItem_clicked(object sender, EventArgs e)
        {
            var proxyItem = sender as MenuItem;
            proxyItem.Checked = true;
            if(currentSelectedProxyMenuItem != null)
            {
                currentSelectedProxyMenuItem.Checked = false;
            }
            ConfigController.Instance.SelecteProxyByName((string)proxyItem.Tag);
            currentSelectedProxyMenuItem = proxyItem;
        }

        private void importFormClipboardMenuitem_clicked(object sender, EventArgs e)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if(dataObject.GetDataPresent(DataFormats.StringFormat))
            {
                string clipboradData = dataObject.GetData(DataFormats.StringFormat).ToString().Trim();
                System.Diagnostics.Debug.Write(clipboradData);
                Proxy proxy = ShareSchemeParser.ParseToProxy(clipboradData);
                if(proxy != null)
                {
                    ConfigController.Instance.AddProxy(proxy);
                    ShowMessage("剪贴板导入", $"{proxy.Name}导入成功");
                }
                else
                {
                    ShowErrorMessage("剪贴板导入", "不支持的格式");
                }
            }
        }

        private void proxyDelayItem_clicked(object sender, EventArgs e)
        {
            if(delayForm == null || delayForm.IsDisposed)
            {
                delayForm = new ProxyDelayForm();
            }
            delayForm.Show();
            delayForm.Activate();
        }

        private void onlineRuleMenuItem_clicked(object sender, EventArgs e)
        {
            if (onlineRuleForm == null || onlineRuleForm.IsDisposed)
            {
                onlineRuleForm = new OnlineRuleForm();
            }
            onlineRuleForm.Show();
            onlineRuleForm.Activate();
        }

        private void editeRuleMenuItem_clicked(object sender, EventArgs e)
        {
            if (!Directory.Exists(AppContract.Path.RULE_DIR))
            {
                Directory.CreateDirectory(AppContract.Path.RULE_DIR);
            }
            if (!File.Exists(AppContract.Path.USER_RULE_PATH))
            {
                File.Create(AppContract.Path.USER_RULE_PATH).Close();
            }

            MessageBox.Show($"请手动编辑{AppContract.Path.USER_RULE_NAME}文件，其规则命中优先级大于在线规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(AppContract.Path.RULE_DIR));
        }

    }
}
