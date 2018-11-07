using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ClashW.Config;
using ClashW.Config.Api;
using ClashW.Config.Api.Dao;
using ClashW.ProcessManager;

namespace ClashW.View
{
    public partial class LogForm : Form
    {
        private ClashApi.LogMessageHandler outPutHandler;
        public LogForm()
        {
            InitializeComponent();
        }

        private void output_event(ClashApi api,  LogMessage logMessage)
        {
            if(this.logTextBox.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new ClashApi.LogMessageHandler(this.output_event), new object[] { api, logMessage });
            } else
            {
                this.logTextBox.AppendText($"{DateTime.Now.ToLocalTime().ToString()} {logMessage.Type} {logMessage.Payload}{Environment.NewLine}");
                this.logTextBox.ScrollToCaret();
            }
        }

        private void logForm_closing(object sender, FormClosingEventArgs e)
        {
            if(outPutHandler != null)
            {
                ConfigController.Instance.StopLoadMessage(outPutHandler);
            }
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            outPutHandler = new ClashApi.LogMessageHandler(output_event);
            ConfigController.Instance.StartLoadMessage(outPutHandler);

        }
    }
}
