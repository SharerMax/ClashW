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
using ClashW.Utils;
using System.Threading;

namespace ClashW
{
    static class Program
    {
        static TrayMenu trayMenu;
       
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(applicationExceptionCause);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(unHandlerExceptionCause);
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
            // var errorMessage = error + Environment.NewLine + "退出程序";
            MessageBox.Show(error, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Application.Exit();
            trayMenu?.Close();
            ClashProcessManager.Instance.Kill();
            Loger.Instance.Close();
            Environment.Exit(-1);
        }

        private static void unHandlerExceptionCause(object sender, UnhandledExceptionEventArgs e)
        {
            Loger.Instance.Write(e.ExceptionObject as Exception);
            trayMenu?.Close();
            ClashProcessManager.Instance.Kill();
            Loger.Instance.Close();
        }

        private static void applicationExceptionCause(object sender, ThreadExceptionEventArgs e)
        {
            Loger.Instance.Write(e.Exception);
            trayMenu?.Close();
            ClashProcessManager.Instance.Kill();
            Loger.Instance.Close();
        }

        private static bool checkClashFile()
        {
            //return false;
            return File.Exists(AppContract.Path.CLASH_EXE_PATH) && File.Exists(AppContract.Path.CLASH_GEOIP_PATH);
        }
    }
}
