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

namespace ClashW.View
{
    public class TrayMenu
    {
        private NotifyIcon notify;
        private MainForm mainForm;
        private LogForm logForm;
        private GeneralConfigForm generalConfigForm;
        private TrafficForm trafficForm;
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

            generalConfigMenuItem = new MenuItem("通用配置...", new EventHandler(generalConfigMenuItem_clicked));
            trafficViewMenuItem = new MenuItem("流量...", new EventHandler(trafficViewMenuItem_clicked));
            contextMenu = new ContextMenu(new MenuItem[] {
                systemProxyMenuItem,
                new MenuItem("-"),
                directRunningModeMenuItem,
                globalRunningModeMenuItem,
                ruleRunningModeMenuItem,
                new MenuItem("-"),
                proxyServerMenuItemGroup,
                new MenuItem("-"),
                logFormMenuItem,
                generalConfigMenuItem,
                trafficViewMenuItem,
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
               
                menuItems = new MenuItem[selectableProxyNameList.Count];

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
            }

            return menuItems;
            
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
    }
}
