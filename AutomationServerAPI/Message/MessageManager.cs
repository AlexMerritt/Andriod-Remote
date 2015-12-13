using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationServerAPI.Message
{
    public class MessageManager
    {
        

        public MessageManager()
        {
            
        }
        
        public static List<Message> ConvertMessages(string s)
        {
            // Reset the messages
            List<Message> messages = new List<Message>();

            string [] tokens = s.Split('#');

            foreach (string token in tokens)
            {
                if (token.Count() > 0)
                {
                    // Since the "#" delimeter has been removed by the split
                    // Char 0 = message type (ie: info, app)
                    // Char 1 = white space
                    // char 2 = start of the data

                    Message m;

                    char action = token[0];

                    if (action == 'a')
                    {
                        m.type = MessageType.App;
                        
                    }
                    else if(action == 'i')
                    {
                        m.type = MessageType.Info;
                    }
                    else
                    {
                        m.type = MessageType.Invalid;
                    }

                    m.data = token.Substring(2);

                    messages.Add(m);
                }
            }

            return messages;
        }

        public static string ConvertMessages(List<Message> messages)
        {
            string output = "";

            foreach(Message message in messages)
            {
                string type;

                if (message.type == MessageType.App)
                    type = "#a";
                else if (message.type == MessageType.Info)
                    type = "#i";
                else
                    type = "#u";

                output += type + " " + message.data;
            }

            return output;
        }
    }
}
