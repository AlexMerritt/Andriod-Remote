using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Sockets;
using AutomationServerAPI;
using AutomationServerAPI.Message;
using AndroidRemote.Util;

namespace AndroidRemote
{
    [Activity(Label = "AndroidRemote", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TcpClient m_mainServerClient;

        Button button;
        Button gameButton;
        ListView applicationListView;

        AppManager appManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            appManager = new AppManager();

            LocateControls();
        }

        private void LocateControls()
        {
            button = FindViewById<Button>(Resource.Id.MyButton);
            gameButton = FindViewById<Button>(Resource.Id.GameButton);

            applicationListView = FindViewById<ListView>(Resource.Id.ApplicationListView);

            button.Click += delegate { ConnectToServer(); };
            gameButton.Click += delegate { GameButtonPressed(); };
            
            applicationListView.ChoiceMode = ChoiceMode.Single;
        }

        private void GameButtonPressed()
        {
            NetworkStream stream = m_mainServerClient.GetStream();
            int id = applicationListView.CheckedItemPosition;
            string appName = (string)applicationListView.SelectedItem;

            App app = appManager.FindApp(id);
            

            

            Network.SendMessage(stream, "#a id=" + app.id);

            Debug.Log("item " + id + " selected");
        }

        protected void ConnectToServer()
        {
            m_mainServerClient = new TcpClient();
            m_mainServerClient.Connect("192.168.1.102", 8001);

            NetworkStream stream = m_mainServerClient.GetStream();

            System.Diagnostics.Debug.Write("Connected to server");

            byte[] buffer = new byte[m_mainServerClient.ReceiveBufferSize];

            stream.BeginRead(buffer, 0, buffer.Length, MainServerReadCallback, buffer);
        }

        void MainServerReadCallback(IAsyncResult ar)
        {
            NetworkStream stream = m_mainServerClient.GetStream();

            Debug.Log("Read Callback");

            try
            {
                string message = Network.RecieveMessage(stream, ar);
                List<string> items = new List<string>();
                List<AutomationServerAPI.Message.Message> messages = MessageManager.ConvertMessages(message);

                foreach(AutomationServerAPI.Message.Message m in messages)
                {
                    if (m.type == MessageType.Info)
                    {
                    }
                    else if (m.type == MessageType.App)
                    {
                        //items.Add(m.data);
                        appManager.CreateApp(m.data);
                    }
                }

                UpdateApplicationItems(appManager.AppNames);

                Debug.Log(message);
            }
            catch (Exception e)
            {
                Debug.Log("Error....." + e.StackTrace);
            }
        }

        void SetText(string text)
        {
        }

        void UpdateApplicationItems(List<string> items)
        {
            RunOnUiThread(delegate {
                ArrayAdapter ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemChecked, items);
                applicationListView.Adapter = ListAdapter;
            });
            
        }
    }
}

