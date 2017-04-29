namespace Client
{
    partial class Client
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.buttonSendMsg = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.linkLabelSave = new System.Windows.Forms.LinkLabel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.buttonSendFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(91, 57);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(128, 20);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "5000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(91, 14);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(128, 20);
            this.textBoxIP.TabIndex = 4;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(235, 17);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(130, 53);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Location = new System.Drawing.Point(18, 100);
            this.textBoxMsg.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(201, 20);
            this.textBoxMsg.TabIndex = 8;
            // 
            // buttonSendMsg
            // 
            this.buttonSendMsg.Location = new System.Drawing.Point(268, 87);
            this.buttonSendMsg.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonSendMsg.Name = "buttonSendMsg";
            this.buttonSendMsg.Size = new System.Drawing.Size(98, 45);
            this.buttonSendMsg.TabIndex = 9;
            this.buttonSendMsg.Text = "向服务器发送消息";
            this.buttonSendMsg.UseVisualStyleBackColor = true;
            this.buttonSendMsg.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(60, 370);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 370);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "状态：";
            // 
            // linkLabelClear
            // 
            this.linkLabelClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelClear.AutoSize = true;
            this.linkLabelClear.Location = new System.Drawing.Point(325, 144);
            this.linkLabelClear.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.Size = new System.Drawing.Size(31, 13);
            this.linkLabelClear.TabIndex = 15;
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.Text = "清空";
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 144);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "收到的消息：";
            // 
            // labelIP
            // 
            this.labelIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(286, 370);
            this.labelIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(0, 13);
            this.labelIP.TabIndex = 16;
            // 
            // linkLabelSave
            // 
            this.linkLabelSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSave.AutoSize = true;
            this.linkLabelSave.Location = new System.Drawing.Point(265, 144);
            this.linkLabelSave.Name = "linkLabelSave";
            this.linkLabelSave.Size = new System.Drawing.Size(55, 13);
            this.linkLabelSave.TabIndex = 17;
            this.linkLabelSave.TabStop = true;
            this.linkLabelSave.Text = "保存数据";
            this.linkLabelSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSave_LinkClicked);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.Color.White;
            this.richTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox.Location = new System.Drawing.Point(12, 170);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(353, 192);
            this.richTextBox.TabIndex = 18;
            this.richTextBox.Text = "";
            // 
            // buttonSendFile
            // 
            this.buttonSendFile.Location = new System.Drawing.Point(224, 97);
            this.buttonSendFile.Name = "buttonSendFile";
            this.buttonSendFile.Size = new System.Drawing.Size(36, 25);
            this.buttonSendFile.TabIndex = 20;
            this.buttonSendFile.Text = "...";
            this.buttonSendFile.UseVisualStyleBackColor = true;
            this.buttonSendFile.Click += new System.EventHandler(this.buttonSendFile_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 393);
            this.Controls.Add(this.buttonSendFile);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.linkLabelSave);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.linkLabelClear);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSendMsg);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Client";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "未连接的客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.Button buttonSendMsg;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabelClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.LinkLabel linkLabelSave;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button buttonSendFile;
    }
}

