using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Config
{
    public class PortErrorEventArgs: EventArgs
    {
        public const int HTTP_PORT_ERROR = 0;
        public const int SOCKS_PORT_ERROR = 1;
        public const int EXTERNAL_CONTROL_PORT = 2;
        public int PortErrorType { get; }
        public int Port { get; }
        public PortErrorEventArgs(int port, int errorType)
        {
            Port = port;
            PortErrorType = errorType;
        }
    }
}
