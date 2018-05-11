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

namespace LTM_TH_02
{
    public partial class Form1 : Form
    {
        Socket server, client;
        byte[] data;
        IPEndPoint ipClient;
        public Form1()
        {
            InitializeComponent();
        }

        private void ButSend_Click(object sender, EventArgs e)
        {
            string text = txtMS.Text;
            listBox1.Items.Add(text);
            txtMS.Text = "";
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add(Encoding.ASCII.GetString(data));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 995);
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            string fileName = dlg.FileName;
            FileInfo fi = new FileInfo(fileName);
            string fileNameandSize = fi.Name + "." + fi.Length;
            byte[] fileContents = File.ReadAllBytes(fileName);
            Stream stream = client.GetStream();
            stream.SetLength(fi.Length);//If i set the file length here am getting an exception
            stream.Write(fileContents, 0, fileContents.Length);
            client.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 995);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipServer);
            server.Listen(10);
            client = server.Accept();
            data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add(Encoding.ASCII.GetString(data));
        }
    }
}
