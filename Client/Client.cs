
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetAddressIP();

            label2.Text = "Binding Port:";
        }

        void GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            labelIP.Text = AddressIP;
        }



        Socket clientSocket = null;

        private const int _maxCount = 1024;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(textBoxIP.Text);

                int port = int.Parse(textBoxPort.Text);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(new IPEndPoint(ip, port));

            }
            catch (Exception ex)
            {
                labelInfo.Text = ex.Message;
                return;
            }


            new Thread(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[_maxCount];
                    int length = 0;
                    try
                    {
                        do
                        {
                            length = clientSocket.Available;
                            Thread.Sleep(5);
                        } while (length < clientSocket.Available);

                        if (length > 0)
                        {
                            buffer = new byte[length];

                            clientSocket.Receive(buffer, 0, length, SocketFlags.None);

                            var s = (Encoding.ASCII.GetString(buffer));

                            richTextBox.AppendText(s + Environment.NewLine);
                        }
                    }
                    catch (Exception)
                    {
                        Text = "已断开连接";

                        labelInfo.Text = "已从服务器断开连接";
                    }
                }

            })
            { IsBackground = true }.Start();

            buttonConnect.Enabled = false;

            Text = "已连接到服务器";

            labelInfo.Text = "已连接到服务器";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var msg = Encoding.ASCII.GetBytes(textBoxMsg.Text);

            clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, null, null);

        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            richTextBox.Clear();
        }

        private void linkLabelSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            File.WriteAllText(DateTime.Now.ToString("Client - yyyyMMdd-HHmmssfff") + ".txt", richTextBox.Text);
        }
    }
}
