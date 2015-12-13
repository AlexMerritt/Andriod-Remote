using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using AutomationServerAPI;

namespace ComputerAutomationServer
{
    class Server
    {
        
        protected int m_port;
        protected string m_ipAddress;

        public string IpAddress
        {
            get { return m_ipAddress; }
        }

        public int Port
        {
            get { return m_port; }
        }

        protected TcpListener m_listener;

        protected void WaitForClientConnect()
        {
            m_listener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
        }

        virtual protected void OnClientConnect(IAsyncResult ar)
        {
            try
            {
                TcpClient client = default(TcpClient);
                client = m_listener.EndAcceptTcpClient(ar);

                ClientConnected(client);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error...." + e.StackTrace);
            }

            WaitForClientConnect();
        }

        protected virtual void ClientConnected(TcpClient client)
        {

        }

        public virtual void MessageRecieved(HandleClientRequest handle, string message)
        {

        }

        public void ClientDisconnected()
        {
            Log("Client Disconnected");
        }

        protected void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}
