using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;


namespace SocketBase
{
    public class UDPServerEx
    {
        bool _bExit = false;
        Queue<byte []> _queneMouseEvnet = new Queue<byte[]>();
        //접속된 클라이언트 정보를 관리 한다.
        Dictionary<string, IPEndPoint> _dicClient = new Dictionary<string, IPEndPoint>();
        byte[] _data = new byte[2048];
        int _port = 9000;
        IPEndPoint  _ipep;
        UdpClient   _server;

        IReceiveEvent _IRecevieEvnet = null;
        System.Threading.Thread _thReceive = null;
        System.Threading.Thread _thQueue = null;

        ManualResetEvent _eventQuene = new ManualResetEvent(false);
        

        public UDPServerEx()
        {
        }
        public void SetIRecevieCallBack(IReceiveEvent inter)
        {
            _IRecevieEvnet = inter;
        }

        public bool InitSocket()
        {
            _ipep = new IPEndPoint(IPAddress.Any, _port);
            _server = new UdpClient(_ipep);
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
        public bool StartQueue()
        {
            if (_thQueue == null)
            {
                _thQueue = new System.Threading.Thread(_thSend);
                _thQueue.Start();
            }
            return true;
        }
        public bool Stop()
        {
            _bExit = true;
            _eventQuene.Reset();

            if (_thReceive != null)
            {
                _thReceive.Join();
            }
            if (_thQueue != null)
            {
                _thQueue.Join();
            }
            return _bExit;
        }
        void _thRun()
        {
            while (!_bExit)
            {
                IPEndPoint clients = null;
                try
                {
                    _data = _server.Receive(ref clients);
                }
                catch (ObjectDisposedException ex)
                {
                    _bExit = true;
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }
                catch(SocketException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                    _bExit = true;
                }
                string key = clients.ToString();

                //처음 접속된 클라이언트 이면 리스트에 추가 한다. 
                if (!_dicClient.ContainsKey(key))
                {
                    System.Diagnostics.Trace.WriteLine(clients.ToString() + " 접속 되었습니다.");
                    _dicClient.Add(key, clients);
                }
                
                if (_data.Length != 0)
                {
                    if (_IRecevieEvnet != null)
                    {
                        string msg = Encoding.Default.GetString(_data);
                        _IRecevieEvnet.ReveiveEvent(this, _data, _data.Length, msg);
                    }
                }
            }
        }

        void _thSend()
        {
            while(!_bExit)
            {
                _eventQuene.WaitOne(); //데이터가 없으면 작업을 진행 하지 않는다.
                while(true)
                {
                    if(_queneMouseEvnet.Count > 0)
                    {
                        byte[] data = null;
                        lock (_queneMouseEvnet) //Queue 를 동기화 한다.ㅣ
                        {
                            data = _queneMouseEvnet.Dequeue();
                        }
                        Send(data);
                    }
                    else
                    {
                        break;
                    }
                }
               
                if(!_bExit)
                {
                    _eventQuene.Reset();
                }
            }
            
        }

        public int AddSendData(byte [] data)
        {
            lock(_queneMouseEvnet) //Queue 를 동기화 한다.ㅣ
            {
                this._queneMouseEvnet.Enqueue(data);
            }
            _eventQuene.Set();
            return 0;
        }

        public int Send(byte [] data,int len)
        {
            
            //foreach(IPEndPoint endp in _dicClient.Values)
            foreach(string key in _dicClient.Keys)
            {
                IPEndPoint endp = _dicClient[key];
                try
                {
                    _server.Send(data, len, endp);
                }
                catch(Exception ex)
                {
                    
                }
                
            }
            return _dicClient.Count;
        }
        public int Send(byte[] data)
        {
            foreach (IPEndPoint endp in _dicClient.Values)
            {
                _server.Send(data, data.Length, endp);
            }
            return _dicClient.Count;
        }
        public int Send(string message)
        {
            byte[] data = Encoding.Default.GetBytes(message);
            foreach (IPEndPoint endp in _dicClient.Values)
            {
                _server.Send(data, data.Length, endp);
            }
            return _dicClient.Count;
        }

        public void Close()
        {
            _server.Close();
        }
    }
}
