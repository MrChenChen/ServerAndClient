
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



namespace Server
{
    public partial class Server : Form
    {
        object _lockKey = new object();

        byte[] buffer = new byte[MAXLENGTH];

        const int MAXLENGTH = 8192;

        List<Socket> list_socket = new List<Socket>();



        List<byte> ReceiveFile = new List<byte>(10000);

        bool IsRecrvieFile = false;

        long FileLenght = 0;

        string ReceiveFilename = String.Empty;



        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetAddressIP();

            label2.Text = "Binding Port:";
        }

        void GetAddressIP()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            textBoxIP.Text = AddressIP;
        }


        private void ReceiveMessage(IAsyncResult ar)
        {
            var socket = ar.AsyncState as Socket;

            try
            {
                lock (_lockKey)
                {
                    var length = socket.EndReceive(ar);

                    listBoxClient.SelectedIndex = list_socket.IndexOf(socket);

                    byte[] buffer_copy = new byte[length];

                    Array.Copy(buffer, buffer_copy, length);

                    HandleReceiveData(buffer_copy);

                    socket.BeginReceive(buffer, 0, MAXLENGTH, SocketFlags.None, ReceiveMessage, socket);
                }
            }
            catch (Exception ex)
            {
                labelInfo.Text = ex.Message;

                listBoxClient.Items.RemoveAt(list_socket.IndexOf(socket));

                list_socket.Remove(socket);
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
                    MessageBox.Show(this, "接收文件完成，文件大小： " + ReceiveFile.Count + Environment.NewLine + "文件保存在当前路径 ： " + ReceiveFilename, "服务器");
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

        private void button1_Click(object sender, EventArgs e)
        {

            int port = int.Parse(textBoxPort.Text);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(ipe);
            s.Listen(0);

            labelInfo.Text = "创建服务器成功";

            Text = "创建服务器成功";

            buttonOpenServer.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Socket tempClient = s.Accept();

                    list_socket.Add(tempClient);

                    new Thread(new ThreadStart(() =>
                    {
                        var client = list_socket[list_socket.Count - 1];

                        string tag = (list_socket.Count - 1).ToString();

                        listBoxClient.Items.Add(tag + "  号客户端");

                        listBoxClient.SelectedIndex = listBoxClient.Items.Count - 1;

                        client.BeginReceive(buffer, 0, MAXLENGTH, SocketFlags.None, ReceiveMessage, client);

                    }))
                    { IsBackground = true }.Start();
                }
            }))
            { IsBackground = true }.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkBoxSendAll.Checked)
            {
                if (listBoxClient.Items.Count > 0)
                {
                    list_socket[listBoxClient.SelectedIndex].Send(Encoding.Unicode.GetBytes(textBoxMsg.Text));
                }
            }
            else
            {
                if (listBoxClient.Items.Count > 0)
                {
                    foreach (var item in list_socket)
                    {
                        item.Send(Encoding.Unicode.GetBytes(textBoxMsg.Text));
                    }
                }
            }
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            richTextBox.Clear();
        }

        private void labelIP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(textBoxIP.Text);
        }

        private void listBoxClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxClient.SelectedIndex >= 0)
            {
                OpenFileDialog openfile = new OpenFileDialog();

                openfile.CheckFileExists = true;

                openfile.Filter = "所有文件|*.*";

                openfile.Multiselect = false;

                var socket = list_socket[listBoxClient.SelectedIndex];

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

        private void linkLabelSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            File.WriteAllText(DateTime.Now.ToString("Server - yyyyMMdd-HHmmssfff") + ".txt", richTextBox.Text);
        }



    }
}
