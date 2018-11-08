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
            configEditor.SetListenedHttpPort(httpPort);
            configEditor.SetListenedSocksPort(socksPort);
            configEditor.AllowLan(allowLan);
            configEditor.SetExternalController(externalController);
            configEditor.SetExteranlControllerSecret(externalControllerSecret);
            configEditor.SetLogLevel(logLevel);
            configEditor.Commit();
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GeneralConfigForm_Load(object sender, EventArgs e)
        {
            socksNumericUpDown.Value = configController.GetListenedSocksPort();
            httpNumericUpDown.Value = configController.GetListenedHttpProt();
            allowLanCheckBox.Checked = configController.IsAllowLan();
            externalContrallTextbox.Text = configController.GetExternalController();
            externalSecretTextbox.Text = configController.GetExternalControllerSecret();
            switch(configController.GetLogLevel())
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
    }
}
