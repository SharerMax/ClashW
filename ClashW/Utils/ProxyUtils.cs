using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Utils
{
    public sealed class ProxyUtils
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        public static void SetProxy(string proxyhost, bool proxyEnabled)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subkey;

            Registry.SetValue(keyName, "ProxyServer", proxyhost);
            Registry.SetValue(keyName, "ProxyEnable", proxyEnabled ? 1 : 0);
            Registry.SetValue(keyName, "ProxyOverride", "localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;172.32.*;192.168.*");

            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        public static bool ProxyEnabled(string proxyhost)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subkey;
            return Registry.GetValue(keyName, "ProxyServer", string.Empty).Equals(proxyhost) && (int)Registry.GetValue(keyName, "ProxyEnable", 0) == 1;

        }
    }
}
