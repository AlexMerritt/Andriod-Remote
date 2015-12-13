using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationServerAPI.Message
{
    public enum MessageType
    {
        App,
        Info,
        Invalid
    };

    public struct Message
    {
        public Message(string data, MessageType type)
        {
            this.data = data;
            this.type = type;
        }

        public string data;
        public MessageType type;
    }
}
