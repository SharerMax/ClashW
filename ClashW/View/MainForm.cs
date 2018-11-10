using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClashW.ProcessManager;
using ClashW.Config;
using ClashW.Config.Yaml.Dao;

namespace ClashW.View
{
    public partial class MainForm : Form
    {
        private string currentEditProxyType;

        #region 代理类型
        private const string PROXY_TYPE_HTTP = "HTTP";
        private const string PROXY_TYPE_HTTPS = "HTTPS";
        private const string PROXY_TYPE_SOCKS5 = "SOCKS5";
        private const string PROXY_TYPE_SHADOWSOCKS = "SHADOWSOCKS";
        private const string PROXY_TYPE_VMESS = "VMESS";
        #endregion

        #region clash 运行模式
        private const string RUNNING_MODE_RULE = "Rule";
        private const string RUNNING_MODE_DIRECT = "Direct";
        private const string RUNNING_MODE_GLOBAL = "Global";
        #endregion

        private Proxy defaultProxy;
        private List<Proxy> proxyList;
        private RunningMode runningMode;
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchEditProxyConfigPanel(((ComboBox)sender).SelectedItem.ToString().ToUpper());
        }

        private void SwitchEditProxyConfigPanel(String type)
        {
            switch (type)
            {
                case PROXY_TYPE_HTTP:
                    currentEditProxyType = PROXY_TYPE_HTTP;
                    HideSSConfigPanel();
                    HideTLSConfigPanel();
                    HideVmessConfigPanel();
                    break;
                case PROXY_TYPE_HTTPS:
                    currentEditProxyType = PROXY_TYPE_HTTPS;
                    HideSSConfigPanel();
                    HideTLSConfigPanel();
                    HideVmessConfigPanel();
                    break;
                case PROXY_TYPE_SOCKS5:
                    currentEditProxyType = PROXY_TYPE_SOCKS5;
                    ShowTLSConfigPanel();
                    HideSSConfigPanel();
                    HideVmessConfigPanel();
                    break;
                case PROXY_TYPE_SHADOWSOCKS:
                    currentEditProxyType = PROXY_TYPE_SHADOWSOCKS;
                    HideTLSConfigPanel();
                    ShowSSConfigPanel();
                    HideVmessConfigPanel();
                    break;
                case PROXY_TYPE_VMESS:
                    currentEditProxyType = PROXY_TYPE_VMESS;
                    HideSSConfigPanel();
                    ShowTLSConfigPanel();
                    ShowVmessConfigPanel();
                    break;
                default:
                    HideSSConfigPanel();
                    HideTLSConfigPanel();
                    HideVmessConfigPanel();
                    break;
            }
        }

        private void HideSSConfigPanel()
        {
            if(ssConfigPanel.Visible)
            {
                ssConfigPanel.Visible = false;
            }
        }

        private void ShowSSConfigPanel()
        {
            if (!ssConfigPanel.Visible)
            {
                ssConfigPanel.Visible = true;
            }
        }

        private void HideTLSConfigPanel()
        {
            if(tlsConfigPanel.Visible)
            {
                tlsConfigPanel.Visible = false;
            }
        }

        private void ShowTLSConfigPanel()
        {
            if(!tlsConfigPanel.Visible)
            {
                tlsConfigPanel.Visible = true;
            }
        }

        private void ShowVmessConfigPanel()
        {
            if(!vmessConfigPanel.Visible)
            {
                vmessConfigPanel.Visible = true;
            }
        }

        private void HideVmessConfigPanel()
        {
            if (vmessConfigPanel.Visible)
            {
                vmessConfigPanel.Visible = false;
            }
        }

        private void saveConfigButton_Click(object sender, EventArgs e)
        {
            // LogForm logForm = new LogForm();
            // logForm.Show();
            var name = nameTextBox.Text.Trim();
            var server = serverTextBox.Text.Trim();
            var serverPort = (int)serverPoxtNumericUpDown.Value;

            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(server) || serverPort == 0 || String.IsNullOrEmpty(currentEditProxyType))
            {
                MessageBox.Show("有未填写或选择的选项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            foreach (Proxy proxy in proxyList)
            {
                if (proxy.Name.Equals(name))
                {
                    MessageBox.Show("代理名不可以重复", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            var newProxy = new Proxy();
            newProxy.Name = name;
            newProxy.Server = server;
            newProxy.Port = serverPort;
            switch (currentEditProxyType)
            {
                case PROXY_TYPE_HTTP:
                    newProxy.Type = "http";
                    break;
                case PROXY_TYPE_HTTPS:
                    newProxy.Type = "https";
                    break;
                case PROXY_TYPE_SOCKS5:
                    newProxy.Type = "socks5";
                    newProxy.TLS = tlsCheckBox.Checked;
                    newProxy.SkipCertVerify = tlsSkipVerifyCheckBox.Checked;
                    break;
                case PROXY_TYPE_SHADOWSOCKS:
                    var cipher = ssCipherComboBox.SelectedItem.ToString().Trim();
                    var password = ssPasswordTextBox.Text.Trim();
                    var obfs = ssObfsTypeTextBox.Text.Trim();
                    var obfsHost = ssObfsHostTextBox.Text.Trim();
                    if(String.IsNullOrEmpty(cipher) || String.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("有未填写或选择的选项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    newProxy.Type = "ss";
                    newProxy.Cipher = cipher;
                    newProxy.Password = password;
                    if(!String.IsNullOrEmpty(obfs))
                    {
                        newProxy.Obfs = obfs;
                    }
                    if(!String.IsNullOrEmpty(obfsHost))
                    {
                        newProxy.ObfsHost = obfsHost;
                    }

                    break;
                case PROXY_TYPE_VMESS:
                    newProxy.Type = "vmess";
                    newProxy.Uuid = vmessUUIDTextBox.Text.Trim();
                    newProxy.AlterId = vmessAlterIDTextBox.Text.Trim();
                    newProxy.Network = vmessNetworkTypeComboBox.SelectedItem as string;
                    newProxy.TLS = tlsCheckBox.Checked;
                    newProxy.SkipCertVerify = tlsSkipVerifyCheckBox.Checked;
                    if(newProxy.Network.Equals("WS"))
                    {
                        newProxy.WsPath = vmessWsPathTextBox.Text.Trim();
                    }
                    
                    break;
                default:
                    break;

            }


            proxyList = ConfigController.Instance.AddProxy(newProxy);
            defaultProxy = null;
            refreshProxyList();
            proxyListBox.SelectedIndex = proxyList.Count - 1;

        }

        private bool checkTypedConfigLegal()
        {
            if(String.IsNullOrEmpty(currentEditProxyType))
            {
                return false;
            }
            var legal = false;
            return legal;
        }

        protected override void OnClosed(EventArgs e)
        {
            ClashProcessManager.Instance.Kill();
            base.OnClosed(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            reInitComponent();
        }

        private void reInitComponent()
        {
            proxyList = ConfigController.Instance.GetProxyList();
            runningMode = ConfigController.Instance.GetRunningMode();
            
            typeComboBox.SelectedIndex = 0;
            vmessChiperComboBox.SelectedIndex = 0;
            vmessNetworkTypeComboBox.SelectedIndex = 0;

            if (proxyList != null)
            {
                refreshRunningModeUI();
                refreshProxyList();
                proxyListBox.SelectedIndex = 0;
            }
        }

        private void refreshRunningModeUI()
        {
            switch (runningMode)
            {
                case RunningMode.RULE:
                    ruleModeRadioButton.Checked = true;
                    break;
                case RunningMode.DIRECT:
                    directModeRadioButton.Checked = true;
                    break;
                case RunningMode.GLOBAL:
                    globalModeRadioButton.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void refreshProxyList()
        {
            proxyListBox.Items.Clear();
            
            if (proxyList.Count == 0)
            {
                if(defaultProxy == null)
                {
                    defaultProxy = generateDefaultProxy();
                }
                proxyList.Add(defaultProxy);
            }
            foreach (Proxy proxy in proxyList)
            {
                proxyListBox.Items.Add(proxy.Name);
            }
        }

        private Proxy generateDefaultProxy()
        {
            Proxy defaultProxy = new Proxy();
            defaultProxy.Name = "未配置";
            defaultProxy.Port = 1;
            return defaultProxy;
        }

        private void directModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkModeChanged(RUNNING_MODE_DIRECT))
            {
                if (defaultProxy != null)
                {
                    MessageBox.Show("有未保存的代理", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    refreshRunningModeUI();
                    return;
                }
                runningMode = RunningMode.DIRECT;
                ConfigController.Instance.SwitchRunningMode(runningMode);
            }
        }

        private void globalModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
           if(checkModeChanged(RUNNING_MODE_GLOBAL))
            {
                if (defaultProxy != null)
                {
                    
                    MessageBox.Show("有未保存的代理", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    refreshRunningModeUI();
                    return;
                }
                runningMode = RunningMode.GLOBAL;
                ConfigController.Instance.SwitchRunningMode(runningMode);
            }
            
        }

        private void ruleModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (checkModeChanged(RUNNING_MODE_RULE))
            {
                if (defaultProxy != null)
                {
                    MessageBox.Show("有未保存的代理", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    refreshRunningModeUI();
                    return;
                }
                runningMode = RunningMode.RULE;
                ConfigController.Instance.SwitchRunningMode(runningMode);
            }
        }

        private bool checkModeChanged(string mode)
        {
            var unChanged = false;
            switch(mode)
            {
                case RUNNING_MODE_DIRECT:
                    unChanged = (directModeRadioButton.Checked && runningMode == RunningMode.DIRECT) || (!directModeRadioButton.Checked && !(runningMode == RunningMode.DIRECT));
                    break;
                case RUNNING_MODE_GLOBAL:
                    unChanged = (globalModeRadioButton.Checked && runningMode == RunningMode.GLOBAL) || (!globalModeRadioButton.Checked && !(runningMode == RunningMode.GLOBAL));
                    break;
                case RUNNING_MODE_RULE:
                    unChanged = (ruleModeRadioButton.Checked && runningMode == RunningMode.RULE) || (!ruleModeRadioButton.Checked && !(runningMode == RunningMode.RULE));
                    break;
                default:
                    break;

            }
            return !unChanged;
        }

        private void proxyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(proxyListBox.SelectedIndex == -1)
            {
                return;
            }
            Proxy proxy = proxyList[proxyListBox.SelectedIndex];
            if(proxy != defaultProxy)
            {
                nameTextBox.Text = proxy.Name;
                serverTextBox.Text = proxy.Server;
                serverPoxtNumericUpDown.Value = proxy.Port;
            }
           
            switch(proxy.Type)
            {
                case "http":
                    typeComboBox.SelectedIndex = 0;
                    break;
                case "https":
                    typeComboBox.SelectedIndex = 1;
                    break;
                case "socks5":
                    typeComboBox.SelectedIndex = 2;
                    break;
                case "ss":
                    typeComboBox.SelectedIndex = 3;
                    ssCipherComboBox.SelectedItem = proxy.Cipher;
                    ssPasswordTextBox.Text = proxy.Password;
                    ssObfsHostTextBox.Text = proxy.Obfs;
                    ssObfsTypeTextBox.Text = proxy.ObfsHost;
                    break;
                case "vmess":
                    typeComboBox.SelectedIndex = 4;
                    break;
                default:
                    break;
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var selectedIndex = proxyListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                return;
            }

            var deletedProxy = proxyList[selectedIndex];
            if(deletedProxy == defaultProxy)
            {
                proxyList.Remove(defaultProxy);
                defaultProxy = null;
            } else
            {
                proxyList = ConfigController.Instance.RemoveProxy(proxyList[selectedIndex]);
            }
            refreshProxyList();
            if(proxyList.Count > selectedIndex)
            {
                proxyListBox.SelectedIndex = selectedIndex;
            } else
            {
                proxyListBox.SelectedIndex = proxyList.Count - 1;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(defaultProxy != null)
            {
                return;
            }
            defaultProxy = generateDefaultProxy();
            proxyList.Add(defaultProxy);
            refreshProxyList();
            proxyListBox.SelectedIndex = proxyList.Count - 1;
        }

        private void vmessNetworkTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(vmessNetworkTypeComboBox.SelectedItem.Equals("WS"))
            {
                vmessWsPathTextBox.Enabled = true;
            }
            else
            {
                vmessWsPathTextBox.Enabled = false;
            }
        }
    }
}
