using ClashW.Config;
using ClashW.Config.Yaml.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClashW.Config.Api.ClashApi;

namespace ClashW.View
{
    public partial class ProxyDelayForm : Form
    {
        private ProxyDelayHandler proxyDelayHandler;
        private Dictionary<string, ListViewItem> proxyDelayPairs;
        private int requestDelayCount;
        public ProxyDelayForm()
        {
            InitializeComponent();
            proxyDelayHandler = new ProxyDelayHandler(proxyDelayHandle);
        }

        private void ProxyDelayForm_Load(object sender, EventArgs e)
        {
            List<Proxy> proxies = ConfigController.Instance.GetProxyList();
            if (proxyDelayPairs == null)
            {
                proxyDelayPairs = new Dictionary<string, ListViewItem>();
            }
            if(proxyDelayPairs.Count > 0)
            {
                proxyDelayPairs.Clear();
            }
            foreach (Proxy proxy in proxies)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = proxy.Name;
                listViewItem.SubItems.Add("等待...");
                proxyDelayListView.Items.Add(listViewItem);
                proxyDelayPairs.Add(proxy.Name, listViewItem);
                reTestButton.Enabled = false;
                requestDelayCount++;
                ConfigController.Instance.RequestProxyDelay(proxy, proxyDelayHandler);
            }
        }

        private void proxyDelayHandle(string name, int delay)
        {
            if(proxyDelayListView.InvokeRequired)
            {
                Invoke(proxyDelayHandler, new object[] { name, delay });
            }
            else
            {
                requestDelayCount--;
                if (proxyDelayPairs != null)
                {
                    ListViewItem listViewItem = proxyDelayPairs[name];
                    listViewItem.SubItems.Clear();
                    listViewItem.Text = name;
                    listViewItem.SubItems.Add(delay.ToString());
                }
                if (requestDelayCount == 0)
                {
                    reTestButton.Enabled = true;
                }
                else
                {
                    reTestButton.Enabled = false;
                }
            }
           
        }

        private void ReTestButton_Click(object sender, EventArgs e)
        {
            if(proxyDelayPairs != null)
            {
                foreach (KeyValuePair<string, ListViewItem> entry in proxyDelayPairs)
                {
                    string name = entry.Key;
                    ListViewItem listViewItem = entry.Value;
                    listViewItem.SubItems.Clear();
                    listViewItem.Text = name;
                    listViewItem.SubItems.Add("等待...");
                    reTestButton.Enabled = false;
                    requestDelayCount++;
                    ConfigController.Instance.RequestProxyDelayByName(name, proxyDelayHandler);
                }
            }
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
