using ClashW.Config.Yaml.Dao;
using ClashW.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ClashW.Utils
{
    public sealed class ShareSchemeParser
    {
        private static readonly Regex sip001Regex = new Regex(@"(?<method>\S+):(?<password>\S+)@(?<host>\S+):(?<port>\d+)");
        private ShareSchemeParser() { }

        public static Proxy ParseToProxy(string data)
        {
            if(String.IsNullOrEmpty(data))
            {
                return null;
            }

            if(data.StartsWith("ss://"))
            {
                return ParseToShadowsocksProxy(data);
            }

            if(data.StartsWith("vmess://"))
            {
                return ParseToVmessProxy(data);
            }

            if(data.StartsWith("http://") || data.StartsWith("https://"))
            {
                return ParseToHttpProxy(data);
            }

            if(data.StartsWith("socks5://"))
            {
                return ParseToSocks5Proxy(data);
            }

            return null;
           
        }

        private static Proxy ParseToShadowsocksProxy(string data)
        {
            
            Proxy proxy = new Proxy();
            if(data.Contains("@"))
            {
                Uri uri = new Uri(data);
                // SIP002 https://shadowsocks.org/en/spec/SIP002-URI-Scheme.html
                proxy.Server = uri.Host;
                proxy.Port = uri.Port;

                //string userInfo = uri.UserInfo.Replace('_', '/').Replace('-', '+');
                string userInfo = uri.UserInfo;

                var decodeUserInfoBytes = System.Convert.FromBase64String(userInfo.PadRight(userInfo.Length + (4 - userInfo.Length % 4) % 4, '='));
                string decodeUserInfo = System.Text.Encoding.UTF8.GetString(decodeUserInfoBytes);
                var decodeUserInfos = decodeUserInfo.Split(':');
                if(decodeUserInfos.Length == 2)
                {
                    proxy.Cipher = decodeUserInfos[0].ToUpper();
                    proxy.Password = decodeUserInfos[1];
                }
                proxy.Name = String.IsNullOrEmpty(uri.Fragment) || uri.Fragment.Length < 1 ? $"{uri.Host}:{uri.Port}" : uri.Fragment.Substring(1);
                return proxy;
            }
            else
            {
                // http://shadowsocks.org/en/config/quick-guide.html
                var nameIndex = data.IndexOf('#');
                if(nameIndex != -1 && nameIndex < (data.Length - 1))
                {
                    proxy.Name = HttpUtility.UrlDecode(data.Substring(nameIndex + 1));
                    data = data.Substring(5, nameIndex - 5);
                }
                else
                {
                    data = data.Substring(5);
                }

                // https://github.com/shadowsocks/shadowsocks-windows/blob/0bd1389350f5792777607fbe312a00954e62d0e3/shadowsocks-csharp/Model/Server.cs#L99
                // base64长度必须为4的倍数，不足的使用'='填充
                // (4 - data.Length % 4) 不足长度数
                // 若base64 已经满足条件，上一步会导致 +1，未防止此种行为 其上一步可在对4取余
                var decodeDataBytes = System.Convert.FromBase64String(data.PadRight(data.Length + (4 - data.Length % 4) % 4, '='));
                var decodeData = System.Text.Encoding.UTF8.GetString(decodeDataBytes);
                Match match = sip001Regex.Match(decodeData);
                if(match.Success)
                {
                    var groups = match.Groups;
                    proxy.Server = groups["host"].Value;
                    proxy.Port = Convert.ToInt32(groups["port"].Value);
                    //var splitedUserInfo = uri.UserInfo.Split(':');
                    proxy.Cipher = groups["method"].Value.ToUpper();
                    proxy.Password = groups["password"].Value;
                    if (String.IsNullOrEmpty(proxy.Name))
                    {
                        proxy.Name = $"{proxy.Server}:{proxy.Port}";
                    }
                    return proxy;
                }
                else
                {
                    return null;
                }
            }
        }

        private static Proxy ParseToVmessProxy(string data)
        {
            // vmess://
            data = data.Substring(8).Trim();
            var padedData = data.PadRight(data.Length + (4 - data.Length % 4) % 4, '=');
            try
            {
                var decodeDataBytes = System.Convert.FromBase64String(padedData);
                var decodeData = System.Text.Encoding.UTF8.GetString(decodeDataBytes);
                Vmess vmess = JsonConvert.DeserializeObject<Vmess>(decodeData);
                if(vmess != null && vmess.Version == 2)
                {
                    if(vmess.Net.Equals("tcp") || vmess.Net.Equals("ws") && vmess.Type.Equals("none"))
                    {
                        Proxy proxy = new Proxy();
                        proxy.Type = "vmess";
                        proxy.Name = vmess.Name;
                        proxy.Server = vmess.Host;
                        proxy.Port = vmess.Port;
                        proxy.Cipher = "Auto";
                        proxy.Uuid = vmess.Uuid;
                        proxy.AlterId = vmess.AlterId;
                        proxy.TLS = vmess.Tls.Equals("tls");
                        if(proxy.TLS)
                        {
                            proxy.SkipCertVerify = true;
                        }
                        if(vmess.Net.Equals("ws"))
                        {
                            proxy.Network = "ws";
                            proxy.WsPath = vmess.ObfsPath;
                        }
                        return proxy;
                    }
                }
            }
            catch (FormatException e)
            {
                Loger.Instance.Write(e);
            }
            return null;
        }

        private static Proxy ParseToHttpProxy(string data)
        {
            Uri uri = new Uri(data);
            Proxy proxy = new Proxy();
            proxy.Type = uri.Scheme;
            proxy.Server = uri.Host;
            proxy.Port = uri.Port;
            proxy.Name = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
            return proxy;
        }

        private static  Proxy ParseToSocks5Proxy(string data)
        {
            // socks5://host:port?tls=true&skipVerify=true
            Uri uri = new Uri(data);
            Proxy proxy = new Proxy();
            proxy.Type = "socks5";
            proxy.Server = uri.Host;
            proxy.Port = uri.Port;
            proxy.Name = $"socks5://{uri.Host}:{uri.Port}";

            var queryParams = String.IsNullOrEmpty(uri.Query) ? null : uri.Query.Split('&');
            if(queryParams != null && queryParams.Length > 0)
            {
                foreach (string entry in queryParams) {
                    var keyValue = entry.Split('=');
                    if(keyValue.Length > 1)
                    {
                        if(keyValue[0].Equals("tls"))
                        {
                            proxy.TLS = Convert.ToBoolean(keyValue[1]);
                        }

                        if(keyValue[0].Equals("skipVerify"))
                        {
                            proxy.SkipCertVerify = Convert.ToBoolean(keyValue[1]);
                        }
                    }
                }
            }
            return proxy;
        }

        class Vmess
        {
            [JsonProperty("v")]
            public int Version { set; get; }
            [JsonProperty("ps")]
            public string Name { set; get; }
            [JsonProperty("add")]
            public string Host { set; get; }
            [JsonProperty("port")]
            public int Port { set; get; }
            [JsonProperty("id")]
            public string Uuid { set; get; }
            [JsonProperty("aid")]
            public string AlterId { set; get; }
            [JsonProperty("net")]
            public string Net { set; get; }
            [JsonProperty("type")]
            public string Type { set; get; }
            [JsonProperty("host")]
            public string ObfsHost { set; get; }
            [JsonProperty("path")]
            public string ObfsPath { set; get; }
            [JsonProperty("tls")]
            public string Tls { set; get; }
        }
    }
}
