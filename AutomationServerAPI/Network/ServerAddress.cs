using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationServerAPI
{
    class ServerAddress
    {
        string m_ipAddress;
        int m_port;

        public string IpAddress
        {
            get { return m_ipAddress; }
        }

        public int Port
        {
            get { return m_port; }
        }

        public ServerAddress(string ipAddress, int port)
        {
            m_ipAddress = ipAddress;
            m_port = port;
        }
    }
}
