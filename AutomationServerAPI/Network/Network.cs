using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace AutomationServerAPI
{
    public class Network
    {
        public static bool SendMessage(NetworkStream stream, string message)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);

            stream.Write(sendBytes, 0, sendBytes.Length);
            stream.Flush();

            return true;
        }

        public static string RecieveMessage(NetworkStream stream, IAsyncResult ar)
        {
            string output = "";

            int read = stream.EndRead(ar);
            if (!(read == 0))
            {
                byte[] buffer = ar.AsyncState as byte[];
                output = Encoding.ASCII.GetString(buffer, 0, read);
            }

            return output;
        }
    }
}
