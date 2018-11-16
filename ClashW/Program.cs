using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClashW.View;
using ClashW.Config;
using ClashW.ProcessManager;
using System.Drawing;
using ClashW.Properties;
using ClashW.Config.Yaml;
using ClashW.Log;
using System.IO;

namespace ClashW
{
    static class Program
    {
        static TrayMenu trayMenu;
        private const string CLASH_TARGET_NAME = @"./clash-win64.exe";
        private const string GEOIP_TARGET_NAME = @"./Country.mmdb";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(applicationExceptionCause);
            if(checkClashFile())
            {
                start();
            }
            else
            {
                var downF = new DownloadClashForm();
                DialogResult result = downF.ShowDialog();
                if(result == DialogResult.OK)
                {
                    start();
                } else
                {
                    Application.Exit();
                }
            }
        }

        static void showMainForm(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            mainForm.Show();
            mainForm.Activate();
        }

        private static void start()
        {
            var isDefaultConfig = !ConfigController.CheckYamlConfigFileExists();
            ConfigController.EnsureRunningConfig();
            var clashProcessManager = ClashProcessManager.Instance;
            clashProcessManager.ProcessErrorEvnet += new ClashProcessManager.ProcessErrorHandler(clashProcessError);
            clashProcessManager.Start();
            ConfigController.Instance.Init(clashProcessManager);
            trayMenu = new TrayMenu();
            trayMenu.Show();
            Application.ApplicationExit += new EventHandler(application_exit);
            trayMenu.ShowMessage("Running", "ClashW已启动");
            if(isDefaultConfig)
            {
                ShowDefaultGeneralConfigForm();
            }
            
            Application.Run();
        }

        private static void ShowDefaultGeneralConfigForm()
        {
            GeneralConfigForm generalConfigForm = new GeneralConfigForm();
            generalConfigForm.ShowDialog();
            MainForm mainForm = new MainForm();
            mainForm.Show();
            mainForm.Activate();
        }

        private static void application_exit(object sender, EventArgs e)
        {
            trayMenu?.Close();
            ClashProcessManager.Instance.Kill();
            Loger.Instance.Close();
        }

        private static void clashProcessError(ClashProcessManager clashProcessManager, string error)
        {
           
            if(trayMenu != null)
            {
                trayMenu.ShowErrorMessage("ERROR", error);
            }
            var errorMessage = error + Environment.NewLine + "退出程序";
            MessageBox.Show(error, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
            Environment.Exit(-1);
            
        }

        private static void applicationExceptionCause(object sender, System.Threading.ThreadExceptionEventArgs threadExceptionEventArgs)
        {
            Loger.Instance.Write(threadExceptionEventArgs.Exception);
        }

        private static bool checkClashFile()
        {
            //return false;
            return File.Exists(CLASH_TARGET_NAME) && File.Exists(GEOIP_TARGET_NAME);
        }
    }
}
