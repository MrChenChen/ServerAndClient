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


        Socket client = null;
        private void button1_Click(object sender, EventArgs e)
        {

            int port = int.Parse(textBoxPort.Text);
            string host = textBoxIP.Text;

            ///创建终结点EndPoint
            IPAddress ip = IPAddress.Parse(host);

            IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndpoint实例

            ///创建socket并连接到服务器
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建Socket
            try
            {
                client.Connect(ipe);//连接到服务器
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是服务器没有开启！\r\n" + ex.Message);
                return;
            }


            labelInfo.Text = "连接服务器成功";

            buttonConnect.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                byte[] recvBytes = new byte[1024];

                bool IsFirst = true;


                //接收文件 相关变量

                bool IsFileStart = false;


                int fileLength = 0;

                byte[] temp_Bytes = new byte[] { };

                FileStream recfile;

                while (true)
                {
                    try
                    {

                        recvBytes = new byte[1024];

                        if (IsFileStart)
                        {
                            recvBytes = new byte[fileLength];
                        }

                        client.Receive(recvBytes);
                    }
                    catch
                    {
                        labelInfo.Text = "远程主机强行关闭了当前连接！";

                        client.Close();

                        buttonConnect.Enabled = true;

                        Text = "未连接的客户端";

                        return;
                    }

                    if (IsFileStart)
                    {

                        temp_Bytes = recvBytes;

                        IsFileStart = false;

                        continue;
                    }



                    var str = Encoding.Unicode.GetString(recvBytes).Replace("\0", " ").TrimEnd();

                    if (IsFirst && str.StartsWith("#_"))
                    {
                        int i = int.Parse(str.Remove(0, 2));

                        Text = i.ToString() + " 号客户端";

                        IsFirst = false;
                    }
                    else if (str.StartsWith("&") && str.EndsWith("#") && str.Length > 3)
                    {
                        IsFileStart = true;

                        fileLength = int.Parse(str.Substring(1, str.Length - 2));

                        temp_Bytes = new byte[fileLength];
                    }
                    else if (str.StartsWith("#") && str.EndsWith("&") && str.Length > 3)
                    {

                        if (!Directory.Exists("D:\\recevice"))
                        {
                            Directory.CreateDirectory("D:\\recevice");
                        }

                        recfile = new FileStream("D:\\recevice\\" + str.Substring(1, str.Length - 2), FileMode.Create);

                        recfile.Write(temp_Bytes, 0, (int)fileLength);


                        recfile.Flush();
                        recfile.Close();

                        IsFileStart = false;


                        fileLength = 0;

                        temp_Bytes = new byte[] { };

                        recvBytes = new byte[1024];

                        recfile = null;

                        labelInfo.Text = (Environment.TickCount / 1000.0).ToString();

                    }
                    else if (!IsFileStart)
                    {
                        listBoxMsg.Items.Add(str + " - From Server");

                    }

                }

            }))
            { IsBackground = true }.Start();



            new Thread(new ThreadStart(() =>
            {
                UdpClient udp = new UdpClient();

                IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000);

                udp.Connect(iep);

                byte[] buffer = new byte[1024];

                while (true)
                {
                    buffer = udp.Receive(ref iep);

                    var str = Encoding.Unicode.GetString(buffer).Replace("\0", " ").TrimEnd();

                    listBoxMsg.Items.Add(str + " - From UDP");
                }

            }
            ))
            { IsBackground = true }.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.Unicode.GetBytes(textBoxMsg.Text));
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listBoxMsg.Items.Clear();
        }
    }
}
