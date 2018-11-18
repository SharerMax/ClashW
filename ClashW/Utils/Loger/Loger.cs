using ClashW.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Log
{
    public sealed class Loger
    {
        private static object _lock = new object();
        private string logFilePath = AppContract.CLASHW_LOG_PATH;
        private StreamWriter logFileStreamWriter = null;
        public string LogFilePath
        {
            get
            {
                return logFilePath;
            }
            set
            {
                if(!value.Equals(logFilePath) && logFileStreamWriter !=null)
                {
                    logFileStreamWriter.Flush();
                    logFileStreamWriter.Close();
                    var logFileStream = File.Open(logFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    logFileStream.SetLength(0);
                    logFileStreamWriter = new StreamWriter(logFileStream);
                }
                logFilePath = value;
            }
        }
        private Loger()
        {
            if(!Directory.Exists(AppContract.LOG_DIR))
            {
                Directory.CreateDirectory(AppContract.LOG_DIR);
            }
            var logFileStream = File.Open(logFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            logFileStream.SetLength(0);
            logFileStreamWriter = new StreamWriter(logFileStream);
        }

        public static Loger Instance
        {
            get
            {
                return LogInstanceHolder.instance;
            }
        }

        private class LogInstanceHolder
        {
            internal static readonly Loger instance = new Loger();
        }

        public void Write(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            lock(_lock)
            {
                if(logFileStreamWriter != null)
                {
                    logFileStreamWriter.WriteLine(msg);
                }
            }
        }

        public void Write(Exception exception)
        {
            Exception innerException = null;
            do
            {
                Write(exception.StackTrace);
                innerException = exception.InnerException;
            } while (innerException != null);
        }

        public void Close()
        {
            logFileStreamWriter?.Flush();
            logFileStreamWriter?.Close();
            logFileStreamWriter = null;
        }
    }
}
