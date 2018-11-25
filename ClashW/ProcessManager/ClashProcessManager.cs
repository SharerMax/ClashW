using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using ClashW.Log;
using ClashW.Utils;

namespace ClashW.ProcessManager
{
    public sealed class ClashProcessManager
    {
        private static ClashProcessManager clashProcessManager = null;
        private static readonly object padlock = new object();
        private Process process = null;
        private static readonly string PROMCSS_RUN_PATH = AppContract.Path.CLASH_EXE_PATH;

        public delegate void OutPutHandler(string output);
        public event OutPutHandler OutputEvent;

        public delegate void ProcessStartHandler(ClashProcessManager sender);
        public event ProcessStartHandler ProcessStartEvent;

        public delegate void ProcessExitHandler(ClashProcessManager sender);
        public event ProcessExitHandler ProcessExitEvent;

        public delegate void ProcessErrorHandler(ClashProcessManager sender, string message);
        public event ProcessErrorHandler ProcessErrorEvnet;

        public delegate void ProcessRestartHandler(ClashProcessManager sender);
        public event ProcessRestartHandler ProcessRestartEvent;

        public bool IsRunning
        {
            get
            {
                return process != null && !process.HasExited;
            }
        }

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
                process.StartInfo.WorkingDirectory = AppContract.Path.BIN_DIR;
                process.StartInfo.FileName = PROMCSS_RUN_PATH;
                process.StartInfo.Arguments = @"-d .";
                process.OutputDataReceived += new DataReceivedEventHandler(process_data_received);
                process.ErrorDataReceived += new DataReceivedEventHandler(process_data_received);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                ProcessStartEvent?.Invoke(this);
                // process.WaitForExit();
            }
        }

        private void ensureSingleRun()
        {
            var processArray = Process.GetProcessesByName(AppContract.Path.CLASH_PROCESS_NAME);
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
            process = null;
        }

        public void Restart()
        {
            Kill();
            Start();
            ProcessRestartEvent?.Invoke(this);
        }

        private void process_data_received(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if(!String.IsNullOrEmpty(dataReceivedEventArgs.Data))
            {
                string rawMessages = dataReceivedEventArgs.Data;
                Loger.Instance.Write(rawMessages);
                string regex = @"time=(\S+) level=(\S+) msg=(.+)";
                Match matched = Regex.Match(rawMessages, regex);
                string time = matched.Groups[1].Value;
                string loglevel = matched.Groups[2].Value.ToUpper();
                string msg = matched.Groups[3].Value;
                
                if (loglevel.Equals("FATAL"))
                {
                    ProcessErrorEvnet?.Invoke(this, msg);
                    Kill();
                }
                var outputData = rawMessages + Environment.NewLine;
                // ThreadPool.QueueUserWorkItem(writeLogToFile, outputData);
                
                OutputEvent?.Invoke(outputData);
            }
        }
    }
}
