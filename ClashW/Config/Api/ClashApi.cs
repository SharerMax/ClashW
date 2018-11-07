using ClashW.Config.Api.Dao;
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
        public Thread loadLogMessageThread;
        private volatile bool stopLoadLogMessage = true;
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
                    //restClient.UseSynchronizationContext = true;
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

        public void LoadLogMessage()
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
                            System.Diagnostics.Debug.WriteLine(exception.Message);
                        }
                        
                    } while (byteRead > 0 && !stopLoadLogMessage);
                }
            }
        }
    }
}
