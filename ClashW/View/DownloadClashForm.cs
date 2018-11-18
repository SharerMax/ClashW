using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Tar;
using ClashW.Utils;

namespace ClashW.View
{
    public partial class DownloadClashForm : Form
    {
        private const string CLASH_DOWNLOAD_URL = "https://github.com/Dreamacro/clash/releases/download/v0.9.1/clash-win64.zip";
        private const string GEOIP_DOWNLOAD_URL = "https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country.tar.gz";
 
        private WebClient clashWebClient;
        private WebClient geoipWebClient;
        private int completeCount;
       
        public DownloadClashForm()
        {
            InitializeComponent();
        }

        private void DownloadClash_Load(object sender, EventArgs e)
        {
            ensureUsedDirectory();
            clashWebClient = generateWebClient();
            clashWebClient.DownloadFileAsync(new Uri(CLASH_DOWNLOAD_URL), AppContract.CLASH_DOWNLOAD_PATH);
            geoipWebClient = generateWebClient();
            geoipWebClient.DownloadFileAsync(new Uri(GEOIP_DOWNLOAD_URL), AppContract.GEOIP_DOWNLOAD_PATH);
        }

        private WebClient generateWebClient()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadProgressChanged);
            return webClient;
        }

        private void ensureUsedDirectory()
        {
            if (!Directory.Exists(AppContract.DOWNLOAD_TEMP_DIR))
            {
                Directory.CreateDirectory(AppContract.DOWNLOAD_TEMP_DIR);
            }

            if(!Directory.Exists(AppContract.BIN_DIR))
            {
                Directory.CreateDirectory(AppContract.BIN_DIR);
            }
        }

        private void downloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (sender is WebClient)
            {
                if (e.Error != null)
                {
                    showErrorMessageBox(e.Error.Message);
                }

                if(sender == clashWebClient)
                {
                    unZipClashFile();
                } 
                else if(sender == geoipWebClient)
                {
                    unTGZGeoIpFile();
                }

                completeCount++;
                var webClient = sender as WebClient;
                webClient.Dispose();

                if(completeCount >=2)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void unZipClashFile()
        {
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(AppContract.CLASH_DOWNLOAD_PATH, AppContract.DOWNLOAD_TEMP_DIR, string.Empty);
            if(File.Exists(AppContract.CLASH_EXE_PATH))
            {
                File.Delete(AppContract.CLASH_EXE_PATH);
            }
            File.Move($"{AppContract.DOWNLOAD_TEMP_DIR}clash-win64.exe", AppContract.CLASH_EXE_PATH);
        }

        private void unTGZGeoIpFile()
        {
            Stream inStream = File.OpenRead(AppContract.GEOIP_DOWNLOAD_PATH);
            Stream gzipStream = new GZipInputStream(inStream);

            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(AppContract.DOWNLOAD_TEMP_DIR);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
           
            string[] directorPath = Directory.GetDirectories(AppContract.DOWNLOAD_TEMP_DIR);
            for(var index = 0; index < directorPath.Length; index++)
            {
                System.Diagnostics.Debug.WriteLine(directorPath[index]);
                if (directorPath[index].Contains(@"GeoLite2-Country_"))
                {
                    string[] filePath = Directory.GetFiles(directorPath[index]);
                    for(var fileIndex = 0; fileIndex < filePath.Length; fileIndex++)
                    {
                        System.Diagnostics.Debug.WriteLine(filePath[index]);
                        if (filePath[fileIndex].EndsWith(".mmdb"))
                        {
                            if(File.Exists(AppContract.CLASH_GEOIP_PATH))
                            {
                                File.Delete(AppContract.CLASH_GEOIP_PATH);
                            }
                            File.Move(filePath[fileIndex], AppContract.CLASH_GEOIP_PATH);
                            break;
                        }
                    }
                    Directory.Delete(directorPath[index], true);
                    break;
                }
            }
          
        }

        private void downloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (sender is WebClient)
            {
                var webClient = sender as WebClient;
                if (webClient == clashWebClient)
                {
                    clashProgressBar.Value = e.ProgressPercentage;
                    clashProgressValueLabel.Text = $"下载-{e.ProgressPercentage}%";
                }
                else if (webClient == geoipWebClient)
                {
                    geoipProgressBar.Value = e.ProgressPercentage;
                    geoipProgressValuelabel.Text = $"下载-{e.ProgressPercentage}%";
                }
            }
        }

        private void showErrorMessageBox(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            clashWebClient?.CancelAsync();
            geoipWebClient?.CancelAsync();
            clashWebClient?.Dispose();
            geoipWebClient?.Dispose();
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
