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
    class HandleClientRequest
    {
        TcpClient m_client;
        Server m_server;
        NetworkStream m_stream = null;

        uint m_id;

        public uint ID
        {
            get { return m_id; }
        }

        public HandleClientRequest(uint id, TcpClient client, Server server)
        {
            m_id = id;
            m_client = client;
            m_server = server;
        }

        public void StartClient()
        {
            m_stream = m_client.GetStream();

            WaitForRequest();
        }

        private void WaitForRequest()
        {
            // This checks if there is an error to determine if the client has disconnected
            // Need to figure out a check to determine when a client disconnects
            try
            {
                byte[] buffer = new byte[m_client.ReceiveBufferSize];

                m_stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
            catch (Exception e)
            {
                m_server.ClientDisconnected();
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            NetworkStream stream = m_client.GetStream();

            try
            {
                string message = Network.RecieveMessage(stream, ar);
                

                m_server.MessageRecieved(this, message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error....." + e.StackTrace);
            }

            WaitForRequest();
        }

        public void SendMessage(DataPacket packet)
        {
            throw new NotImplementedException();

            NetworkStream stream = m_client.GetStream();

            byte[] sendBytes = DataPacket.ConvertToBytes(packet);
            stream.Write(sendBytes, 0, sendBytes.Length);
            stream.Flush();
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = m_client.GetStream();

            Network.SendMessage(stream, message);
        }
    }
}
