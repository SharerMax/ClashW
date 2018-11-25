using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Utils
{
    public sealed class PortUtils
    {
        private PortUtils() { }
        public static bool TcpPortIsUsed(int port)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();
            foreach(TcpConnectionInformation tcpConnection in tcpConnections)
            {
                if(port == tcpConnection.LocalEndPoint.Port)
                {
                    return true;
                }
                System.Diagnostics.Debug.WriteLine("Local endpoint:  ", tcpConnection.LocalEndPoint.ToString());
                //System.Diagnostics.Debug.WriteLine("Remote endpoint:  ", tcpConnection.RemoteEndPoint.ToString());
            }

            return false;
        }
    }
}
