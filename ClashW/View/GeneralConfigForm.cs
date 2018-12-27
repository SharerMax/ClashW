using ClashW.Config;
using ClashW.Utils;
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
    public partial class GeneralConfigForm : Form
    {
        private ConfigController configController;
        private ConfigEditor configEditor;

        public GeneralConfigForm()
        {
            InitializeComponent();
            configController = ConfigController.Instance;
            configEditor = ConfigController.GetConfigEditor();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
            var httpPort = (int)httpNumericUpDown.Value;
            var socksPort = (int)socksNumericUpDown.Value;
            var externalController = externalContrallTextbox.Text.Trim();
            var externalControllerSecret = externalSecretTextbox.Text.Trim();
            var allowLan = allowLanCheckBox.Checked;
            var logLevel = LogLevel.INFO;
            switch(logLevelComboBox.SelectedText)
            {
                case "INFO":
                    logLevel = LogLevel.INFO;
                    break;
                case "WARNING":
                    logLevel = LogLevel.WARNING;
                    break;
                case "ERROR":
                    logLevel = LogLevel.ERROR;
                    break;
                case "DEBUG":
                    logLevel = LogLevel.DEBUG;
                    break;
                default:
                    break;

            }
            var selfHttpPort = configController.GetListenedHttpProt();
            var selfSocksPort = configController.GetListenedSocksPort();
            var selfExternalPort = Convert.ToInt32(configController.GetExternalController().Split(':')[1]);
            if (selfHttpPort != httpPort && PortUtils.TcpPortIsUsed(httpPort))
            {
                MessageBox.Show($"端口冲突（{httpPort}）", "端口冲突", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selfSocksPort != socksPort && PortUtils.TcpPortIsUsed(socksPort))
            {
                MessageBox.Show($"端口冲突（{socksPort}）", "端口冲突", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var extrnalPort = Convert.ToInt32(externalController.Split(':')[1]);

            if (selfExternalPort != extrnalPort && PortUtils.TcpPortIsUsed(extrnalPort))
            {
                MessageBox.Show($"端口冲突（{extrnalPort}）", "端口冲突", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            configEditor.SetListenedHttpPort(httpPort);
            configEditor.SetListenedSocksPort(socksPort);
            configEditor.AllowLan(allowLan);
            configEditor.SetExternalController(externalController);
            configEditor.SetExteranlControllerSecret(externalControllerSecret);
            configEditor.SetLogLevel(logLevel);
            configEditor.Commit();
            DialogResult = DialogResult.OK;
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }

        private void GeneralConfigForm_Load(object sender, EventArgs e)
        {
            socksNumericUpDown.Value = configController.GetListenedSocksPort();
            httpNumericUpDown.Value = configController.GetListenedHttpProt();
            allowLanCheckBox.Checked = configController.IsAllowLan();
            externalContrallTextbox.Text = configController.GetExternalController();
            externalSecretTextbox.Text = configController.GetExternalControllerSecret();
            testUrlTextBox.Text = Properties.Settings.Default.TestUrl;
            switch (configController.GetLogLevel())
            {
                case LogLevel.INFO:
                    logLevelComboBox.SelectedItem = ConfigController.LOG_LEVEL_INFO.ToUpper();
                    break;
                case LogLevel.WARNING:
                    logLevelComboBox.SelectedItem = ConfigController.LOG_LEVEL_WARING.ToUpper();
                    break;
                case LogLevel.ERROR:
                    logLevelComboBox.SelectedItem = ConfigController.LOG_LEVEL_ERROR.ToUpper();
                    break;
                case LogLevel.DEBUG:
                    logLevelComboBox.SelectedItem = ConfigController.LOG_LEVEL_DEBUG.ToUpper();
                    break;
            }
        }

        private void GeneralConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.CloseReason);
        }

        private void GeneralConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
