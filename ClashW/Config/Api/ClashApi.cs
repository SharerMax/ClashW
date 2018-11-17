using ClashW.Config.Api.Dao;
using ClashW.Log;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClashW.Config.Api
{
    public class ClashApi
    {
        private RestClient restClient;
        private string baseUrl;
        public delegate void LogMessageHandler(ClashApi clashApi, LogMessage logMessage);
        public event LogMessageHandler LogMessageOutputEvent;
        public delegate void TrafficInfoHandler(ClashApi clashApi, TrafficInfo trafficInfo);
        public event TrafficInfoHandler TrafficInfoEvent;
        public Thread loadLogMessageThread;
        public Thread loadTrafficInfoThread;
        public delegate void ProxyDelayHandler(string name, int delay);

        private volatile bool stopLoadLogMessage = true;
        private volatile bool stopLoadTrafficInfo = true;
        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
            set
            {
                baseUrl = value;
                if(restClient != null)
                {
                    // restClient.UseSynchronizationContext = true;
                    restClient.BaseUrl = new Uri(baseUrl);
                }
            }
                
        }

        public ClashApi(string baseUrl)
        {
            restClient = new RestClient(baseUrl);
            this.baseUrl = baseUrl;
        }

        public void SelectedProxy(string name)
        {
            var request = new RestRequest($"proxies/Proxy");
            request.Method = Method.PUT;
            var jsonBody = $"{{\"name\":\"{name}\"}}";
            // https://github.com/restsharp/RestSharp/issues/703
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            var response =  restClient.Execute(request);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public string ProxyInfo(string name)
        {
            var request = new RestRequest($"proxies/{name}");
            request.Method = Method.GET;
            var response =  restClient.Execute(request);
            System.Diagnostics.Debug.WriteLine(response.Content);
            return response.Content;
        }

        public void ProxyDelay(string name, int timeout, string url, ProxyDelayHandler proxyDelayHandler)
        {
            var request = new RestRequest($"proxies/{name}/delay");
            request.Method = Method.GET;
            request.AddQueryParameter("timeout", timeout.ToString());
            request.AddQueryParameter("url", url);
            Loger.Instance.Write($"Request Proxy Delay: {name} - {timeout} - {url}");
            restClient.ExecuteAsync(request, response =>
            {
                int delay = -1;
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    Dictionary<string, int> pairs = JsonConvert.DeserializeObject <Dictionary<string, int>>(response.Content);
                    if(pairs.ContainsKey("delay"))
                    {
                        delay = pairs["delay"];
                    }
                }
                Loger.Instance.Write($"Response Proxy Delay: {name} - {delay}");
                proxyDelayHandler.Invoke(name, delay);
            });
        }

        public void Config(int port, int socksPort, bool allowLan, string mode, string logLevel)
        {
            var request = new RestRequest($"configs");
            request.Method = Method.PUT;
            var jsonBody = $"{{\"port\":{port}, \"socket-port\":{socksPort},\"allow-lan\":${allowLan},\"mode\":\"{mode}\",\"log-level\":\"{logLevel}\"}}";
            // https://github.com/restsharp/RestSharp/issues/703
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public void SwitchMode(string mode)
        {
            var request = new RestRequest($"configs");
            request.Method = Method.PUT;
            var jsonBody = $"{{\"mode\":\"{mode}\"}}";
            // https://github.com/restsharp/RestSharp/issues/703
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public void StartLoadLogMessage()
        {
            if(stopLoadLogMessage)
            {
                stopLoadLogMessage = false;
                loadLogMessageThread = new Thread(new ThreadStart(loadMessage));
                loadLogMessageThread.Start();
            }
        }

        public void StopLoadLogMessage()
        {
            if(!stopLoadLogMessage && loadLogMessageThread.ThreadState != ThreadState.Stopped)
            {
                stopLoadLogMessage = true;
            }
        }

        private void loadMessage()
        {
            WebRequest request = WebRequest.Create($"{baseUrl}/logs");
            request.Method = "GET";
            const int BUFFER_SIZE = 1 * 1024;

            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    
                    int byteRead = -1;
                    do
                    {
                        try
                        {
                            var buffer = new byte[BUFFER_SIZE];
                            byteRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                            var message = Encoding.Default.GetString(buffer);
                            var logMessage = JsonConvert.DeserializeObject<LogMessage>(message);
                            LogMessageOutputEvent?.Invoke(this, logMessage);
                            System.Diagnostics.Debug.WriteLine(message);
                        } catch(Exception exception)
                        {
                            Loger.Instance.Write(exception);
                        }
                        
                    } while (byteRead > 0 && !stopLoadLogMessage);
                }
            }
        }

        public void StartLoadTrafficInfo()
        {
            if (stopLoadTrafficInfo)
            {
                stopLoadTrafficInfo = false;
                loadTrafficInfoThread = new Thread(new ThreadStart(loadTrafficInfo));
                loadTrafficInfoThread.Start();
            }
        }

        public void StopLoadTrafficInfo()
        {
            if (!stopLoadTrafficInfo && loadTrafficInfoThread.ThreadState != ThreadState.Stopped)
            {
                stopLoadTrafficInfo = true;
            }
        }

        private void loadTrafficInfo()
        {
            WebRequest request = WebRequest.Create($"{baseUrl}/traffic");
            request.Method = "GET";
            const int BUFFER_SIZE = 1 * 1024;
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {

                    int byteRead = -1;
                    do
                    {
                        try
                        {
                            var buffer = new byte[BUFFER_SIZE];
                            byteRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                            var trafficInfoJson = Encoding.Default.GetString(buffer);
                            var trafficInfo = JsonConvert.DeserializeObject<TrafficInfo>(trafficInfoJson);
                            TrafficInfoEvent?.Invoke(this, trafficInfo);
                            System.Diagnostics.Debug.WriteLine(trafficInfoJson);
                        }
                        catch (Exception exception)
                        {
                            Loger.Instance.Write(exception);
                        }

                    } while (byteRead > 0 && !stopLoadTrafficInfo);
                }
            }
        }
    }
}
