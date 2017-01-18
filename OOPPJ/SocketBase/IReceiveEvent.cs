using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketBase
{
    public interface IReceiveEvent
    {
        bool ReveiveEvent(object obj,byte[] data, int len,string key);
    }
}
