using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;

namespace Client
{
    public partial class Form1 : Form
    {
        Socket server, client;
        IPEndPoint ipserver;
        byte[] data;
        string n;
        byte[] b1;
        OpenFileDialog op;
        public Form1()
        {
            InitializeComponent();
        }

        private void butConnet_Click(object sender, EventArgs e)
        {
            ipserver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 995);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ipserver);
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            string text = txtMessage.Text;
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            txtMessage.Text = "";
            listBox1.Items.Add(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            text = Encoding.ASCII.GetString(data);
            listBox1.Items.Add(text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
