
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



        Socket serverSocket = null;

        private static byte[] result = new byte[1024];

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(textBoxIP.Text);

                int port = int.Parse(textBoxPort.Text);

                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                serverSocket.Connect(new IPEndPoint(ip, port));
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
                    try
                    {
                        serverSocket.Receive(result, 0, 1024, SocketFlags.None);

                        var s = (Encoding.Unicode.GetString(result) + " - From Server");

                        listBoxMsg.Items.Add(s.Replace("\0", ""));
                    }
                    catch (Exception ex)
                    {
                        labelInfo.Text = ex.Message;
                        serverSocket = null;
                        buttonConnect.Enabled = true;
                        return;
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

            var msg = Encoding.Unicode.GetBytes(textBoxMsg.Text);

            serverSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, null, null);

        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listBoxMsg.Items.Clear();
        }
    }
}
