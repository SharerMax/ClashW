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

namespace ClashW.View
{
    public partial class DownloadClashForm : Form
    {
        private const string CLASH_DOWNLOAD_URL = "https://github.com/Dreamacro/clash/releases/download/v0.9.1/clash-win64.zip";
        private const string GEOIP_DOWNLOAD_URL = "https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country.tar.gz";
        private const string CLASH_DOWNLOAD_ZIP_NAME = @"clash-win64.zip";
        private const string CLASH_DOWNLOAD_TGZ_NAME = @"GeoLite2-Country.tar.gz";
        private const string DOWNLOAD_TEMP_DIRECTORY = @"./temp/";
        private const string CLASH_TARGET_NAME = @"./clash-win64.exe";
        private const string GEOIP_TARGET_NAME = @"./Country.mmdb";
        private WebClient clashWebClient;
        private WebClient geoipWebClient;
        private int completeCount;
       
        public DownloadClashForm()
        {
            InitializeComponent();
        }

        private void DownloadClash_Load(object sender, EventArgs e)
        {
            ensureTempDirectory();
            clashWebClient = generateWebClient();
            clashWebClient.DownloadFileAsync(new Uri(CLASH_DOWNLOAD_URL), $"{DOWNLOAD_TEMP_DIRECTORY}{CLASH_DOWNLOAD_ZIP_NAME}");
            geoipWebClient = generateWebClient();
            geoipWebClient.DownloadFileAsync(new Uri(GEOIP_DOWNLOAD_URL), $"{DOWNLOAD_TEMP_DIRECTORY}{CLASH_DOWNLOAD_TGZ_NAME}");
        }

        private WebClient generateWebClient()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadProgressChanged);
            return webClient;
        }

        private void ensureTempDirectory()
        {
            if (!Directory.Exists(DOWNLOAD_TEMP_DIRECTORY))
            {
                Directory.CreateDirectory(DOWNLOAD_TEMP_DIRECTORY);
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
            fastZip.ExtractZip($"{DOWNLOAD_TEMP_DIRECTORY}{CLASH_DOWNLOAD_ZIP_NAME}", DOWNLOAD_TEMP_DIRECTORY, string.Empty);
            if(File.Exists(CLASH_TARGET_NAME))
            {
                File.Delete(CLASH_TARGET_NAME);
            }
            File.Move($"{DOWNLOAD_TEMP_DIRECTORY}/clash-win64.exe", CLASH_TARGET_NAME);
        }

        private void unTGZGeoIpFile()
        {
            Stream inStream = File.OpenRead($"{DOWNLOAD_TEMP_DIRECTORY}{CLASH_DOWNLOAD_TGZ_NAME}");
            Stream gzipStream = new GZipInputStream(inStream);

            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(DOWNLOAD_TEMP_DIRECTORY);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
           
            string[] directorPath = Directory.GetDirectories($"{DOWNLOAD_TEMP_DIRECTORY}");
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
                            if(File.Exists(GEOIP_TARGET_NAME))
                            {
                                File.Delete(GEOIP_TARGET_NAME);
                            }
                            File.Move(filePath[fileIndex], GEOIP_TARGET_NAME);
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
