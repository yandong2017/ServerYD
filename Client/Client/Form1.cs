using SuperSocket.SocketLuanr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public ClientLink Client = new ClientLink();
        private void btLogin_Click(object sender, EventArgs e)
        {          
            if (!Client.Start())
            {
                MessageBox.Show("连接失败");
                return;
            }

            MessageBox.Show("连接成功");

            string ss = "发消息#7#6#7#ss"; Client.Send(Encoding.Default.GetBytes(ss));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ss = "发消息#7#6#7#ss";
            var msg = new StringBuilder();
            msg.Append(1 + "#");
            msg.Append(12+"#"+2);           

            msg.Append('#' + 1);
            msg.Append('#' + "2121");
            LunarRequestInfo s = new LunarRequestInfo(Encoding.UTF8.GetBytes(ss));
            s.ProtocolID = 1;

            Client.Send(Encoding.Default.GetBytes(s.ToString()));
        }
    }

    public class eventclss
    {
        public int ProtocolId = 0;
    }
}