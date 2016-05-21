﻿using System;
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


namespace Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetAddressIP();
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


        List<Socket> list_socket = new List<Socket>();

        UdpClient udp;

        IPEndPoint udp_ip;

        private void button1_Click(object sender, EventArgs e)
        {


            int port = int.Parse(textBoxPort.Text);
            string host = textBoxIP.Text;

            ///创建终结点（EndPoint）
            IPAddress ip = IPAddress.Parse(host);//把ip地址字符串转换为IPAddress类型的实例
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);//用指定的端口和ip初始化IPEndPoint类的新实例

            ///创建socket并开始监听
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个socket对像，如果用udp协议，则要用SocketType.Dgram类型的套接字
            s.Bind(ipe);//绑定EndPoint对像（2000端口和ip地址）
            s.Listen(0);//开始监听

            labelInfo.Text = "开启服务器成功";

            buttonOpenServer.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Socket tempClient = s.Accept();

                    list_socket.Add(tempClient);

                    tempClient.Send(Encoding.Unicode.GetBytes("#_" + (list_socket.Count - 1).ToString()));

                    new Thread(new ThreadStart(() =>
                    {
                        var client = list_socket[list_socket.Count - 1];

                        string tag = (list_socket.Count - 1).ToString();

                        listBoxClient.Items.Add(tag + " 号服务器接入");

                        listBoxClient.SelectedIndex = listBoxClient.Items.Count - 1;

                        byte[] recvBytes = new byte[1024];

                        while (true)
                        {
                            try
                            {
                                recvBytes = new byte[1024];

                                client.Receive(recvBytes);

                                string t = Encoding.Unicode.GetString(recvBytes).Replace("\0", " ").TrimEnd();

                                listBoxMegs.Items.Add(t + " - From " + tag.ToString() + " 号");
                            }
                            catch
                            {
                                labelInfo.Text = tag + " 号远程关闭了客户端";

                                for (int i = 0; i < listBoxClient.Items.Count; i++)
                                {
                                    if (listBoxClient.Items[i].ToString() == (tag + " 号服务器接入"))
                                    {
                                        listBoxClient.Items.RemoveAt(i);
                                        list_socket.RemoveAt(i);
                                        break;
                                    }
                                }

                                return;
                            }

                        }
                    }))
                    { IsBackground = true }.Start();
                }

            }))
            { IsBackground = true }.Start();


            udp = new UdpClient();

            udp.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000));

        }

        //向指定客户端发送消息
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBoxAll.Checked)
            {

                var b = Encoding.Unicode.GetBytes(textBoxMsg.Text);

                udp.Send(b, b.Length,new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000));

            }
            else
            {
                if (listBoxClient.Items.Count > 0)
                {
                    list_socket[listBoxClient.SelectedIndex].Send(Encoding.Unicode.GetBytes(textBoxMsg.Text));
                }
            }

        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listBoxMegs.Items.Clear();
        }

        private void labelIP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(labelIP.Text);
        }

        private void listBoxClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxClient.SelectedIndex >= 0)
            {
                OpenFileDialog openfile = new OpenFileDialog();

                openfile.CheckFileExists = true;

                openfile.InitialDirectory = @"D:\";

                openfile.Multiselect = false;

                var socket = list_socket[listBoxClient.SelectedIndex];

                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(openfile.FileName, FileMode.Open);

                    labelInfo.Text = (Environment.TickCount / 1000.0).ToString();

                    string msg = "&" + fs.Length.ToString() + "#";
                    socket.Send(Encoding.Unicode.GetBytes(msg));


                    //设置缓冲区为1024byte
                    byte[] buff = new byte[fs.Length];
                    fs.Read(buff, 0, (int)fs.Length);
                    socket.Send(buff, 0, (int)fs.Length, SocketFlags.None);


                    msg = "#" + new FileInfo(openfile.FileName).Name + "&";
                    socket.Send(Encoding.Unicode.GetBytes(msg));
                }

            }
        }
    }
}
