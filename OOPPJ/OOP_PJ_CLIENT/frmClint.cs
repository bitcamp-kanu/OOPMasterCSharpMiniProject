using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SocketBase;
using OOP_PJ;
using SocketBase;

namespace OOP_PJ_CLIENT
{
    public partial class frmClint : Form, IReceiveEvent
    {
        SocketBase.UDPClientEx _clientEx = new UDPClientEx();
        List<Shape> shapes = new List<Shape>();
        public frmClint()
        {
            InitializeComponent();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
            _clientEx.Ip = Properties.Settings.Default.ConnetcIP;
            _clientEx.SetIRecevieCallBack(this);
            _clientEx.InitSocket();

            try
            {
                _clientEx.Send(Packet.Serialize(new Login()));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("오류가 발생 하여 프로그램을 종료 합니다.");
                this.Close();
            }
            _clientEx.StartRecevie();            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (Shape s in shapes)
            {
                s.Draw(e.Graphics);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_clientEx != null)
            {
                //_clientEx.Exit();
                //_clientEx.Close();
            }
            base.OnClosing(e);
        }

        public bool ReveiveEvent(object ojb, byte[] data, int len, string msg)
        {
            object o = Packet.Deserialize(data);
            if (o is StartPacket)
            {
                shapes.Clear();
            }
            else if (o is LastPacket)
            {
                this.Invalidate();
            }
            else if (o is Shape)
            {
                Shape shap = (Shape)o;
                shapes.Add(shap);
            }
            else if (o is SizeChange)
            {
                SizeChange sz = o as SizeChange;

                Action action = delegate() { this.Size = new Size(sz._width, sz._heigth); };
                if (this.InvokeRequired)
                {
                    this.Invoke(action);
                }
                else 
                {
                    action();
                }
            }
            else if (o is Exit)
            {
                Action action = delegate() 
                {
                    this.Close(); 
                };
                this.BeginInvoke(action);//비동기로 호출 한다.
                return false ;
            }
            return true;
        }
    }
}
