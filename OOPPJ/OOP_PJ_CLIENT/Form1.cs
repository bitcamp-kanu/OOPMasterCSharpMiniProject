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

namespace OOP_PJ_CLIENT
{
    public partial class Form1 : Form, IReceiveEvent
    {
        SocketBase.UDPClientEx _clientEx = new UDPClientEx();
        List<Shape> shapes = new List<Shape>();
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _clientEx.Ip = Properties.Settings.Default.ConnetcIP;
            _clientEx.SetIRecevieCallBack(this);
            _clientEx.InitSocket();
            _clientEx.StartRecevie();

            MessagePacket pack = new MessagePacket();
            _clientEx.Send(Packet.Serialize(pack));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (Shape s in shapes)
            {
                s.Draw(e.Graphics);
            }
        }

        public void ReveiveEvent(object ojb, byte[] data, int len, string msg)
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
        }
    }
}
