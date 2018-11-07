using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ClashW.ProcessManager
{
    public sealed class ClashProcessManager
    {
        private static ClashProcessManager clashProcessManager = null;
        private static readonly object padlock = new object();
        private Process process = null;
        private const string PROMCSS_NMAE = @"./clash-win64";
        private const string LOG_FILE = @"./outpout.log";
        private StreamWriter logFileStreamWrite = null;

        public delegate void OutPutHandler(string output);
        public event OutPutHandler OutputEvent;

        public delegate void ProcessStartHandler(ClashProcessManager sender);
        public event ProcessStartHandler ProcessStartEvent;

        public delegate void ProcessExitHandler(ClashProcessManager sender);
        public event ProcessExitHandler ProcessExitEvent;

        public delegate void ProcessErrorHandler(ClashProcessManager sender, string message);
        public event ProcessErrorHandler ProcessErrorEvnet;

        private ClashProcessManager()
        {

        }
        public static ClashProcessManager Instance
        {
            get
            {
                if(clashProcessManager == null)
                {
                    lock(padlock)
                    {
                        if(clashProcessManager == null)
                        {
                            clashProcessManager = new ClashProcessManager();
                        }
                    }
                }
                return clashProcessManager;
            }
        }
        public void Start()
        {
            ensureSingleRun();
            if(process == null || process.HasExited)
            {
                process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = PROMCSS_NMAE;
                process.StartInfo.Arguments = @"-d .";
                process.OutputDataReceived += new DataReceivedEventHandler(process_data_received);
                process.ErrorDataReceived += new DataReceivedEventHandler(process_data_received);
                var logFileStream = File.Open(LOG_FILE, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                logFileStream.SetLength(0);
                logFileStreamWrite = new StreamWriter(logFileStream);
                logFileStreamWrite.AutoFlush = true;
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                ProcessStartEvent?.Invoke(this);
                // process.WaitForExit();
            }
        }

        private void ensureSingleRun()
        {
            var processArray = Process.GetProcessesByName("clash-win64");
            foreach(Process p in processArray) {
                if(!p.HasExited)
                {
                    p.Kill();
                    p.Close();
                }
            }
        }

        public void Kill()
        {
            if(process !=null && !process.HasExited)
            {
                process.Kill();
                process.Close();
                ProcessExitEvent?.Invoke(this);
            }
            logFileStreamWrite.Close();
            process = null;
        }

        public void Restart()
        {
            Kill();
            Start();
        }

        private void process_data_received(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if(!String.IsNullOrEmpty(dataReceivedEventArgs.Data))
            {
                if(dataReceivedEventArgs.Data.StartsWith("ERROR"))
                {
                    ProcessErrorEvnet?.Invoke(this, dataReceivedEventArgs.Data);
                }
                var outputData = dataReceivedEventArgs.Data + Environment.NewLine;
                ThreadPool.QueueUserWorkItem(writeLogToFile, outputData);
                OutputEvent?.Invoke(outputData);
            }
        }

        private void writeLogToFile(object log)
        {
            lock(padlock)
            {
                logFileStreamWrite.Write(log as string);
            }
        }
    }
}
