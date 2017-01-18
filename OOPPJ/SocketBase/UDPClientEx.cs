using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketBase
{
    public class UDPClientEx
    {
        byte[] _data = new byte[2048];
        //string _ip = "127.0.0.1";
        int _port = 9000;
        UdpClient _client;
        IPEndPoint _sender;

        IReceiveEvent _IRecevieEvnet = null;
        System.Threading.Thread _thReceive = null;
        event System.EventHandler SocketErrorHandler;
        public bool IsExit
        {
            get;
            set;
        }

        public string Ip
        {
            get;
            set;           
        }

        public void SetIRecevieCallBack(IReceiveEvent inter)
        {
            _IRecevieEvnet = inter;
        }
        public bool InitSocket()
        {
            _client = new UdpClient(Ip, _port);
            IsExit = false;
            //지정된 아이피와 포트 에서만 데이터를 받는다.
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _sender = new IPEndPoint(IPAddress.Parse(Ip), _port);
            return true;
        }

        public bool StartRecevie()
        {
            if (_thReceive == null)
            {
                _thReceive = new System.Threading.Thread(_thRun);
                _thReceive.Start();
            }
            return true;
        }
        void _thRun()
        {
            while (!IsExit)
            {
                try
                {
                    _data = _client.Receive(ref _sender);
                    if (_data.Length != 0)
                    {
                        if (_IRecevieEvnet != null)
                        {
                            string msg = Encoding.Default.GetString(_data);
                            _IRecevieEvnet.ReveiveEvent(this, _data, _data.Length, msg);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    if (SocketErrorHandler != null)
                    {
                        SocketErrorHandler(this, EventArgs.Empty);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(true,ex.ToString());
                    }
                    //Thread 를 종료 한다.
                    IsExit = true;
                }
                
            }
        }        
        public int Send(byte [] data,int len)
        {
            return _client.Send(data, len);
        }

        public int Send(byte[] data)
        {
            return _client.Send(data, data.Length);
        }
        public int Send(string str)
        {
            byte[] data = Encoding.Default.GetBytes(str);
            return _client.Send(data, data.Length);
        }

        public void Close()
        {
            _client.Close();
        }
    }
}
