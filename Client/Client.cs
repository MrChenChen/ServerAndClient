
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

        object _lockKey = new object();

        Socket serverSocket = null;

        byte[] buffer = new byte[MAXLENGTH];

        const int MAXLENGTH = 8192;

        List<byte> ReceiveFile = new List<byte>(10000);

        bool IsRecrvieFile = false;

        long FileLenght = 0;

        string ReceiveFilename = String.Empty;


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
                serverSocket.BeginReceive(buffer, 0, MAXLENGTH, SocketFlags.None, ReceiveMessage, serverSocket);
            })
            { IsBackground = true }.Start();

            buttonConnect.Enabled = false;

            Text = "已连接到服务器";

            labelInfo.Text = "已连接到服务器";
        }


        private void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                lock (_lockKey)
                {
                    var socket = ar.AsyncState as Socket;

                    var length = socket.EndReceive(ar);

                    byte[] buffer_copy = new byte[length];

                    Array.Copy(buffer, buffer_copy, length);

                    HandleReceiveData(buffer_copy);

                    socket.BeginReceive(buffer, 0, MAXLENGTH, SocketFlags.None, ReceiveMessage, socket);
                }
            }
            catch (Exception ex)
            {
                buttonConnect.Enabled = true;
                labelInfo.Text = ex.Message;
            }
        }

        private void HandleReceiveData(byte[] buffer)
        {
            var s = (Encoding.Unicode.GetString(buffer));

            if (IsRecrvieFile)
            {
                ReceiveFile.AddRange(buffer);

                if (FileLenght == ReceiveFile.Count)
                {
                    File.WriteAllBytes(ReceiveFilename, ReceiveFile.ToArray());
                    IsRecrvieFile = false;
                    FileLenght = 0;
                    MessageBox.Show(this, "接收文件完成，文件大小： " + ReceiveFile.Count + Environment.NewLine + "文件保存在当前路径 ： " + ReceiveFilename, "客户端");
                    ReceiveFile.Clear();
                    ReceiveFilename = string.Empty;
                }
            }
            else
            {
                if (s.First() == 0x02 && s.Last() == 0x03)
                {
                    try
                    {
                        var temp = s.Substring(1, s.Length - 2);

                        ReceiveFilename = temp.Split('|')[0];

                        var len = int.Parse(temp.Split('|')[1]);

                        if (len > 0)
                        {
                            FileLenght = len;
                            IsRecrvieFile = true;

                            richTextBox.AppendText(string.Format("开始接受文件（文件名称：{0}，文件大小： {1}）...", ReceiveFilename, len) + Environment.NewLine);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    richTextBox.AppendText(s + Environment.NewLine);
                }
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {

            var msg = Encoding.Unicode.GetBytes(textBoxMsg.Text);

            serverSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, null, null);

        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            richTextBox.Clear();
        }

        private void linkLabelSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            File.WriteAllText(DateTime.Now.ToString("Client - yyyyMMdd-HHmmssfff") + ".txt", richTextBox.Text);
        }

        private void buttonSendFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();

            openfile.CheckFileExists = true;

            openfile.Filter = "所有文件|*.*";

            openfile.Multiselect = false;

            var socket = serverSocket;

            if (openfile.ShowDialog() == DialogResult.OK)
            {
                var fileinfo = new FileInfo(openfile.FileName);

                richTextBox.AppendText(string.Format("开始发送文件（文件名称：{0}，文件大小： {1}）...", fileinfo.Name, fileinfo.Length));

                labelInfo.Text = (Environment.TickCount / 1000.0).ToString();

                string msg = Encoding.ASCII.GetString(new byte[] { 0x02 })
                    + fileinfo.Name
                    + "|"
                    + fileinfo.Length
                    + Encoding.ASCII.GetString(new byte[] { 0x03 });

                socket.Send(Encoding.Unicode.GetBytes(msg));

                Thread.Sleep(300);

                socket.Send(File.ReadAllBytes(openfile.FileName));

            }

        }

    }
}
