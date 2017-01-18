using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



namespace SocketBase
{
    [Serializable]
    public class Packet
    {
        public static byte[] Serialize(Object data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(1024 * 4); // packet size will be maximum 4k
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }
        public static Object Deserialize(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(1024 * 4);
                ms.Write(data, 0, data.Length);

                ms.Position = 0;
                BinaryFormatter bf = new BinaryFormatter();
                Object obj = bf.Deserialize(ms);
                ms.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }
    }

    [Serializable]
    public class MouseEvnet : Packet
    {
        public int mode; // 0 이면 사각형 1이면 라인.
        public int x;
        public int y;
        public int action;
    }

    [Serializable]
    public class MessagePacket
    {
        public string _message01;
        public string _message02;
    }

    [Serializable]
    public class StartPacket
    {
        public int totolCnt;
    }

    [Serializable]
    public class LastPacket
    {
        public int totolCnt;
    }
}
