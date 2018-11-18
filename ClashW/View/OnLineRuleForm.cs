using ClashW.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashW.View
{
    public partial class OnlineRuleForm : Form
    {
        private int cycleHour = 1;
        public OnlineRuleForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
            
            string url = onlineLinkTextBox.Text.Trim();
            if(String.IsNullOrEmpty(url))
            {
                ConfigController.Instance.SetOnlineRuleUrl(url, cycleHour);
                Close();
                return;
            }

            if(!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                MessageBox.Show("只支持HTTP(S)的链接", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConfigController.Instance.SetOnlineRuleUrl(url, cycleHour);
            Close();
        }

        private void cycle1HRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cycleHour = 1;
        }

        private void cycle6HRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cycleHour = 6;
        }

        private void cycle12HRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cycleHour = 12;
        }

        private void OnlineRuleForm_Load(object sender, EventArgs e)
        {
            onlineLinkTextBox.Text = ConfigController.Instance.GetOnlineRuleUrl();
            switch (ConfigController.Instance.GetOnlineRuleUpdateCycleHour())
            {
                case 1:
                    cycle1HRadioButton.Checked = true;
                    break;
                case 6:
                    cycle6HRadioButton.Checked = true;
                    break;
                case 12:
                    cycle12HRadioButton.Checked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
