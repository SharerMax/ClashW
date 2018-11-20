using ClashW.Log;
using ClashW.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Config.FileSystemWather
{
    
    sealed class UserRuleFileSystemWatcher
    {
        private FileSystemWatcher fileSystemWatcher;
        private FileSystemEventHandler fileSystemEvent;
        private const int shakeThreshold = 1000_000;
        private long preChangeTime;
        private UserRuleFileSystemWatcher()
        {
            if(!Directory.Exists(AppContract.Path.RULE_DIR))
            {
                Directory.CreateDirectory(AppContract.Path.RULE_DIR);
            }
            fileSystemWatcher = new FileSystemWatcher(AppContract.Path.RULE_DIR, AppContract.Path.USER_RULE_NAME);
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemEvent = new FileSystemEventHandler(handleFileSystemEvent);
            //fileSystemWatcher.Created += fileSystemEvent;
            fileSystemWatcher.Changed += fileSystemEvent;
            //fileSystemWatcher.Deleted += fileSystemEvent;
            fileSystemWatcher.Renamed += new RenamedEventHandler(onRenamed);
        }

        private void handleFileSystemEvent(object sender, FileSystemEventArgs e)
        {
            switch(e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    onCreated(sender, e);
                    break;
                case WatcherChangeTypes.Changed:

                    var currentTime = DateTime.Now.ToFileTimeUtc();
                    System.Diagnostics.Debug.Write($"pre: {preChangeTime}");
                    System.Diagnostics.Debug.Write($"cur: {currentTime}");
                    System.Diagnostics.Debug.Write($"cha: {currentTime - preChangeTime}");
                    if(currentTime - preChangeTime > shakeThreshold)
                    {
                        preChangeTime = currentTime;
                        onChanged(sender, e);
                    }
                   
                    break;
                case WatcherChangeTypes.Deleted:
                    onDeleted(sender, e);
                    break;
                default:
                    break;
            }
        }

        public void Start()
        {
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void onCreated(object sender, FileSystemEventArgs e)
        {
            Loger.Instance.Write($"用户规则文件创建:{e.FullPath}");
        }

        private void onChanged(object sender, FileSystemEventArgs e)
        {
            Loger.Instance.Write($"用户规则文件变化:{e.FullPath}");
            ConfigController.Instance.ReLoadRule();
        }

        private void onDeleted(object sender, FileSystemEventArgs e)
        {
            Loger.Instance.Write($"用户规则文件删除:{e.FullPath}");
        }

        private void onRenamed(object sender, FileSystemEventArgs e)
        {
            Loger.Instance.Write($"用户规则文件重命名:{e.FullPath}");
        }

        public static UserRuleFileSystemWatcher Instance
        {
            get
            {
               return UserRuleFileSystemWatcherHolder.Instance;
            }
        }

        private class UserRuleFileSystemWatcherHolder
        {
            internal static readonly UserRuleFileSystemWatcher Instance = new UserRuleFileSystemWatcher();
        }
    }


}
