using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using AutomationServerAPI;
using AutomationServerAPI.Message;
using System.Diagnostics;

namespace ComputerAutomationServer
{
    class MessageServer : Server
    {
        private int m_id;

        List<HandleClientRequest> m_clients;
        uint m_currentClientId;

        ApplicationListManager applicationList;

        public int ID
        {
            get { return m_id; }
        }

        public MessageServer(int serverId)
        {
            m_id = serverId;
            m_ipAddress = Values.Server.IpAddress;
            m_port = Values.Server.Port + serverId;
            
            InitializeServer(serverId);

            InitializeSubSystems();

            Run();

            // Since all of logic is running on other threads we need to keep
            // this thread open so the application does not close
            while (true) { }
        }

        void InitializeServer(int serverId)
        {
            try
            {
                m_currentClientId = 0;
                m_clients = new List<HandleClientRequest>();
                IPAddress ipAd = IPAddress.Parse(m_ipAddress);

                m_listener = new TcpListener(ipAd, m_port);
                
                m_listener.Start();

                Log("Server Initialized");
                Log("Ip Address " + m_ipAddress);
                Log("Port " + m_port);
            }
            catch (Exception e)
            {
                Log("Error...." + e.StackTrace);
            }
        }

        private void InitializeSubSystems()
        {
            applicationList = new ApplicationListManager();
        }

        public void CloseServer()
        {
        }

        public void Run()
        {
            WaitForClientConnect();
        }

        protected override void ClientConnected(TcpClient client)
        {
            HandleClientRequest clRequest = new HandleClientRequest(m_currentClientId, client, this);
            clRequest.StartClient();

            Log("Client " + m_currentClientId  + " connected");

            m_clients.Add(clRequest);

            m_currentClientId++;

            clRequest.SendMessage(InitialConnectMessage());
        }

        private string InitialConnectMessage()
        {
            string output = "";

            List<Message> messages = new List<Message>();

            messages.Add(new Message("Welcome to the server", MessageType.Info));

            for(int i = 0; i < applicationList.ListOfApps.Count; ++i)
            {
                AppData app = applicationList.ListOfApps[i];

                messages.Add(new Message("t=" + app.name + " i=" + i, MessageType.App));
            }

            output = MessageManager.ConvertMessages(messages);

            return output;
        }

        public override void MessageRecieved(HandleClientRequest client, string message)
        {
            if (message == "")
                return;

            string fixedString = client.ID + ": " + message;

            Log(fixedString);

            

            List<Message> messages = MessageManager.ConvertMessages(message);

            foreach(Message m in messages)
            {
                if (m.type == MessageType.App)
                {
                    ApplicationMessage(m.data);
                }
                else if(m.type == MessageType.Info)
                {
                    InfoMessage(client, message);
                }
                else
                {
                    Log("Message and an invalid message type");
                }
            }
        }

        private void ApplicationMessage(string s)
        {
            int indexOfId = s.IndexOf("id");

            if (indexOfId >= 0)
            {
                int id = Convert.ToInt32(s.Substring(indexOfId + 3));

                AppData app = applicationList.ListOfApps[id];

                ApplicationLauncher launcher = new ApplicationLauncher();
                launcher.Start(app.path, app.arguments);

            }
        }

        private void InfoMessage(HandleClientRequest client, string s)
        {
            foreach (HandleClientRequest cl in m_clients)
            {
                if (client.ID != cl.ID)
                    cl.SendMessage(s);
            }
        }
    }
}
