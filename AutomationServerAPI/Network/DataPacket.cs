using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutomationServerAPI
{
    [Serializable]
    public class DataPacket
    {
        int m_id;
        string m_message;

        public string Message
        {
            get { return m_message; }
        }

        public int ID
        {
            get { return m_id; }
        }

        public DataPacket()
        {
        }

        public DataPacket(int id, string message)
        {
            m_id = id;
            m_message = message;
        }

        // Convert to byte string
        public static DataPacket ConvertFromBytes(byte[] arrBytes)
        {
            DataPacket p = new DataPacket();

            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                p = (DataPacket)binForm.Deserialize(memStream);
            }

            return p;
        }

        // Convert from byte string
        public static byte[] ConvertToBytes(DataPacket p)
        {
            byte[] buffer = null;            

            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, p);
                buffer = ms.ToArray();
            }

            return buffer;
        }
    }
}
