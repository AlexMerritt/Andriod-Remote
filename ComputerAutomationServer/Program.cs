using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationServerAPI.Message;

namespace ComputerAutomationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            //TokenizeMessage();

            MessageServer server = new MessageServer(0);
        }

        static void Test()
        {
            string message = "#i sdfasdf#a refrf#p aenfe#c agdga";


            List<Message> ms = MessageManager.ConvertMessages(message);

            string output = MessageManager.ConvertMessages(ms);

            while (true) { }
        }

        static void TokenizeMessage()
        {
            string data ="t=asdfasdf asfsd f=12312";

            Dictionary<string, string> appToekns = new Dictionary<string, string>();

            int currentIdx;
            int nextIdx;

            currentIdx = data.IndexOf('=');

            while (currentIdx != -1)
            {
                string token;
                string value;

                nextIdx = data.IndexOf('=', currentIdx + 1);

                token = data.Substring(currentIdx - 1, 1);

                if (nextIdx == -1)// We are at the end of the string
                {
                    value = data.Substring(currentIdx + 1);
                }
                else
                {
                    value = data.Substring(currentIdx + 1, nextIdx - currentIdx - 2);
                }

                appToekns.Add(token, value);

                // Blah blah blah
                currentIdx = nextIdx;
            }

            while (true) { }
        }
    }
}
