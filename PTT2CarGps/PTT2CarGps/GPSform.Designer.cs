﻿namespace PTT2CarGps
{
    partial class GPSform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SerialTimer = new System.Windows.Forms.Timer(this.components);
            this.infoSS = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl_switchview = new System.Windows.Forms.TabControl();
            this.tabPage_gps = new System.Windows.Forms.TabPage();
            this.CarInfoLBL = new System.Windows.Forms.Label();
            this.CarsLB = new System.Windows.Forms.ListBox();
            this.ClearBTTN = new System.Windows.Forms.Button();
            this.TestBTTN = new System.Windows.Forms.Button();
            this.CarLocationsPB = new System.Windows.Forms.PictureBox();
            this.settingsGB = new System.Windows.Forms.GroupBox();
            this.closeConnectionBTTN = new System.Windows.Forms.Button();
            this.SerialComLB = new System.Windows.Forms.ListBox();
            this.ConnectBTTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.baudRateCBB = new System.Windows.Forms.ComboBox();
            this.refreshComPortBTTN = new System.Windows.Forms.Button();
            this.comPortCBB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_server = new System.Windows.Forms.TabPage();
            this.groupBox_server = new System.Windows.Forms.GroupBox();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.label_connection = new System.Windows.Forms.Label();
            this.groupBox_initiate = new System.Windows.Forms.GroupBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.button_initiate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numeric_port = new System.Windows.Forms.NumericUpDown();
            this.IpAddressTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SetIpBTTN = new System.Windows.Forms.Button();
            this.infoSS.SuspendLayout();
            this.tabControl_switchview.SuspendLayout();
            this.tabPage_gps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CarLocationsPB)).BeginInit();
            this.settingsGB.SuspendLayout();
            this.tabPage_server.SuspendLayout();
            this.groupBox_server.SuspendLayout();

            this.groupBox_initiate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_port)).BeginInit();
            this.SuspendLayout();
            // 
            // SerialTimer
            // 
            this.SerialTimer.Interval = 10;
            this.SerialTimer.Tick += new System.EventHandler(this.ReadSerialTimer_Tick);
            // 
            // infoSS
            // 
            this.infoSS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.infoSS.Location = new System.Drawing.Point(0, 684);
            this.infoSS.Name = "infoSS";
            this.infoSS.Size = new System.Drawing.Size(1011, 22);
            this.infoSS.TabIndex = 2;
            this.infoSS.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(120, 17);
            this.toolStripStatusLabel1.Text = "ConnectionStatusLBL";
            // 
            // tabControl_switchview
            // 
            this.tabControl_switchview.Controls.Add(this.tabPage_gps);
            this.tabControl_switchview.Controls.Add(this.tabPage_server);
            this.tabControl_switchview.Location = new System.Drawing.Point(0, -1);
            this.tabControl_switchview.Name = "tabControl_switchview";
            this.tabControl_switchview.SelectedIndex = 0;
            this.tabControl_switchview.Size = new System.Drawing.Size(1011, 682);
            this.tabControl_switchview.TabIndex = 7;
            // 
            // tabPage_gps
            // 
            this.tabPage_gps.Controls.Add(this.SetIpBTTN);
            this.tabPage_gps.Controls.Add(this.label5);
            this.tabPage_gps.Controls.Add(this.IpAddressTB);
            this.tabPage_gps.Controls.Add(this.CarInfoLBL);
            this.tabPage_gps.Controls.Add(this.CarsLB);
            this.tabPage_gps.Controls.Add(this.ClearBTTN);
            this.tabPage_gps.Controls.Add(this.TestBTTN);
            this.tabPage_gps.Controls.Add(this.CarLocationsPB);
            this.tabPage_gps.Controls.Add(this.settingsGB);
            this.tabPage_gps.Location = new System.Drawing.Point(4, 22);
            this.tabPage_gps.Name = "tabPage_gps";
            this.tabPage_gps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_gps.Size = new System.Drawing.Size(1003, 656);
            this.tabPage_gps.TabIndex = 0;
            this.tabPage_gps.Text = "GPS";
            this.tabPage_gps.UseVisualStyleBackColor = true;
            // 
            // CarInfoLBL
            // 
            this.CarInfoLBL.AutoSize = true;
            this.CarInfoLBL.Location = new System.Drawing.Point(407, 424);
            this.CarInfoLBL.Name = "CarInfoLBL";
            this.CarInfoLBL.Size = new System.Drawing.Size(42, 13);
            this.CarInfoLBL.TabIndex = 12;
            this.CarInfoLBL.Text = "car info";
            // 
            // CarsLB
            // 
            this.CarsLB.FormattingEnabled = true;
            this.CarsLB.Location = new System.Drawing.Point(225, 412);
            this.CarsLB.Name = "CarsLB";
            this.CarsLB.Size = new System.Drawing.Size(172, 160);
            this.CarsLB.TabIndex = 11;
            this.CarsLB.SelectedIndexChanged += new System.EventHandler(this.CarsLB_SelectedIndexChanged_1);
            // 
            // ClearBTTN
            // 
            this.ClearBTTN.Location = new System.Drawing.Point(709, 412);
            this.ClearBTTN.Name = "ClearBTTN";
            this.ClearBTTN.Size = new System.Drawing.Size(75, 23);
            this.ClearBTTN.TabIndex = 10;
            this.ClearBTTN.Text = "Clear";
            this.ClearBTTN.UseVisualStyleBackColor = true;
            this.ClearBTTN.Click += new System.EventHandler(this.ClearBTTN_Click);
            // 
            // TestBTTN
            // 
            this.TestBTTN.Location = new System.Drawing.Point(790, 412);
            this.TestBTTN.Name = "TestBTTN";
            this.TestBTTN.Size = new System.Drawing.Size(75, 23);
            this.TestBTTN.TabIndex = 9;
            this.TestBTTN.Text = "Test";
            this.TestBTTN.UseVisualStyleBackColor = true;
            this.TestBTTN.Click += new System.EventHandler(this.TestBTTN_Click);
            // 
            // CarLocationsPB
            // 
            this.CarLocationsPB.BackColor = System.Drawing.Color.Black;
            this.CarLocationsPB.Location = new System.Drawing.Point(225, 6);
            this.CarLocationsPB.Name = "CarLocationsPB";
            this.CarLocationsPB.Size = new System.Drawing.Size(640, 400);
            this.CarLocationsPB.TabIndex = 8;
            this.CarLocationsPB.TabStop = false;
            this.CarLocationsPB.Paint += new System.Windows.Forms.PaintEventHandler(this.CarLocationsPB_Paint_1);
            // 
            // settingsGB
            // 
            this.settingsGB.Controls.Add(this.closeConnectionBTTN);
            this.settingsGB.Controls.Add(this.SerialComLB);
            this.settingsGB.Controls.Add(this.ConnectBTTN);
            this.settingsGB.Controls.Add(this.label2);
            this.settingsGB.Controls.Add(this.baudRateCBB);
            this.settingsGB.Controls.Add(this.refreshComPortBTTN);
            this.settingsGB.Controls.Add(this.comPortCBB);
            this.settingsGB.Controls.Add(this.label1);
            this.settingsGB.Location = new System.Drawing.Point(8, 6);
            this.settingsGB.Name = "settingsGB";
            this.settingsGB.Size = new System.Drawing.Size(211, 589);
            this.settingsGB.TabIndex = 7;
            this.settingsGB.TabStop = false;
            this.settingsGB.Text = "Settings";
            // 
            // closeConnectionBTTN
            // 
            this.closeConnectionBTTN.Location = new System.Drawing.Point(106, 553);
            this.closeConnectionBTTN.Name = "closeConnectionBTTN";
            this.closeConnectionBTTN.Size = new System.Drawing.Size(99, 23);
            this.closeConnectionBTTN.TabIndex = 7;
            this.closeConnectionBTTN.Text = "Close connection";
            this.closeConnectionBTTN.UseVisualStyleBackColor = true;
            this.closeConnectionBTTN.Click += new System.EventHandler(this.closeConnectionBTTN_Click_1);
            // 
            // SerialComLB
            // 
            this.SerialComLB.FormattingEnabled = true;
            this.SerialComLB.Location = new System.Drawing.Point(6, 130);
            this.SerialComLB.Name = "SerialComLB";
            this.SerialComLB.Size = new System.Drawing.Size(199, 420);
            this.SerialComLB.TabIndex = 6;
            // 
            // ConnectBTTN
            // 
            this.ConnectBTTN.Location = new System.Drawing.Point(84, 101);
            this.ConnectBTTN.Name = "ConnectBTTN";
            this.ConnectBTTN.Size = new System.Drawing.Size(121, 23);
            this.ConnectBTTN.TabIndex = 5;
            this.ConnectBTTN.Text = "Connect";
            this.ConnectBTTN.UseVisualStyleBackColor = true;
            this.ConnectBTTN.Click += new System.EventHandler(this.ConnectBTTN_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Baud rate:";
            // 
            // baudRateCBB
            // 
            this.baudRateCBB.FormattingEnabled = true;
            this.baudRateCBB.Items.AddRange(new object[] {
            "110",
            "150",
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.baudRateCBB.Location = new System.Drawing.Point(84, 74);
            this.baudRateCBB.Name = "baudRateCBB";
            this.baudRateCBB.Size = new System.Drawing.Size(121, 21);
            this.baudRateCBB.TabIndex = 3;
            // 
            // refreshComPortBTTN
            // 
            this.refreshComPortBTTN.Location = new System.Drawing.Point(84, 18);
            this.refreshComPortBTTN.Name = "refreshComPortBTTN";
            this.refreshComPortBTTN.Size = new System.Drawing.Size(121, 23);
            this.refreshComPortBTTN.TabIndex = 2;
            this.refreshComPortBTTN.Text = "Refresh COM ports";
            this.refreshComPortBTTN.UseVisualStyleBackColor = true;
            this.refreshComPortBTTN.Click += new System.EventHandler(this.refreshComPortBTTN_Click_1);
            // 
            // comPortCBB
            // 
            this.comPortCBB.FormattingEnabled = true;
            this.comPortCBB.Location = new System.Drawing.Point(84, 47);
            this.comPortCBB.Name = "comPortCBB";
            this.comPortCBB.Size = new System.Drawing.Size(121, 21);
            this.comPortCBB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM port:";
            // 
            // tabPage_server
            // 
            this.tabPage_server.Controls.Add(this.groupBox_server);
            this.tabPage_server.Controls.Add(this.groupBox_initiate);
            this.tabPage_server.Location = new System.Drawing.Point(4, 22);
            this.tabPage_server.Name = "tabPage_server";
            this.tabPage_server.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_server.Size = new System.Drawing.Size(1003, 656);
            this.tabPage_server.TabIndex = 1;
            this.tabPage_server.Text = "Server";
            this.tabPage_server.UseVisualStyleBackColor = true;
            // 
            // groupBox_server
            // 
            this.groupBox_server.Controls.Add(this.textBox_input);
            this.groupBox_server.Controls.Add(this.label_connection);
            this.groupBox_server.Location = new System.Drawing.Point(8, 117);
            this.groupBox_server.Name = "groupBox_server";
            this.groupBox_server.Size = new System.Drawing.Size(527, 363);
            this.groupBox_server.TabIndex = 19;
            this.groupBox_server.TabStop = false;
            this.groupBox_server.Text = "Server";
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(18, 98);
            this.textBox_input.Multiline = true;
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.ReadOnly = true;
            this.textBox_input.Size = new System.Drawing.Size(494, 247);
            this.textBox_input.TabIndex = 16;
            // 
            // numeric_choose
            // 
            this.groupBox_server.Controls.Add(this.textBox_input);
            this.groupBox_server.Controls.Add(this.label_connection);
            this.groupBox_server.Location = new System.Drawing.Point(8, 117);
            this.groupBox_server.Name = "groupBox_server";
            this.groupBox_server.Size = new System.Drawing.Size(527, 363);
            this.groupBox_server.TabIndex = 19;
            this.groupBox_server.TabStop = false;
            this.groupBox_server.Text = "Server";
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(18, 98);
            this.textBox_input.Multiline = true;
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.ReadOnly = true;
            this.textBox_input.Size = new System.Drawing.Size(494, 247);
            this.textBox_input.TabIndex = 16;
            // 
            // label_connection
            // 
            this.label_connection.AutoSize = true;
            this.label_connection.Location = new System.Drawing.Point(15, 53);
            this.label_connection.Name = "label_connection";
            this.label_connection.Size = new System.Drawing.Size(77, 26);
            this.label_connection.TabIndex = 14;
            this.label_connection.Text = "Server: \r\nConnected to: ";
            // 
            // groupBox_initiate
            // 
            this.groupBox_initiate.Controls.Add(this.textBox_ip);
            this.groupBox_initiate.Controls.Add(this.button_initiate);
            this.groupBox_initiate.Controls.Add(this.label3);
            this.groupBox_initiate.Controls.Add(this.label4);
            this.groupBox_initiate.Controls.Add(this.numeric_port);
            this.groupBox_initiate.Location = new System.Drawing.Point(8, 6);
            this.groupBox_initiate.Name = "groupBox_initiate";
            this.groupBox_initiate.Size = new System.Drawing.Size(168, 105);
            this.groupBox_initiate.TabIndex = 6;
            this.groupBox_initiate.TabStop = false;
            this.groupBox_initiate.Text = "Initiate";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(45, 19);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 20);
            this.textBox_ip.TabIndex = 0;
            // 
            // button_initiate
            // 
            this.button_initiate.Location = new System.Drawing.Point(45, 72);
            this.button_initiate.Name = "button_initiate";
            this.button_initiate.Size = new System.Drawing.Size(100, 23);
            this.button_initiate.TabIndex = 4;
            this.button_initiate.Text = "Connect";
            this.button_initiate.UseVisualStyleBackColor = true;
            this.button_initiate.Click += new System.EventHandler(this.button_initiate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port:";
            // 
            // numeric_port
            // 
            this.numeric_port.Location = new System.Drawing.Point(45, 46);
            this.numeric_port.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numeric_port.Name = "numeric_port";
            this.numeric_port.Size = new System.Drawing.Size(100, 20);
            this.numeric_port.TabIndex = 2;
            // 
            // IpAddressTB
            // 
            this.IpAddressTB.Location = new System.Drawing.Point(488, 552);
            this.IpAddressTB.Name = "IpAddressTB";
            this.IpAddressTB.Size = new System.Drawing.Size(131, 20);
            this.IpAddressTB.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(403, 555);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Set IP address:";
            // 
            // SetIpBTTN
            // 
            this.SetIpBTTN.Location = new System.Drawing.Point(625, 550);
            this.SetIpBTTN.Name = "SetIpBTTN";
            this.SetIpBTTN.Size = new System.Drawing.Size(35, 23);
            this.SetIpBTTN.TabIndex = 15;
            this.SetIpBTTN.Text = "Ok";
            this.SetIpBTTN.UseVisualStyleBackColor = true;
            this.SetIpBTTN.Click += new System.EventHandler(this.SetIpBTTN_Click);
            // 
            // GPSform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 706);
            this.Controls.Add(this.tabControl_switchview);
            this.Controls.Add(this.infoSS);
            this.MinimumSize = new System.Drawing.Size(1019, 732);
            this.Name = "GPSform";
            this.Text = "Car gps";
            this.infoSS.ResumeLayout(false);
            this.infoSS.PerformLayout();
            this.tabControl_switchview.ResumeLayout(false);
            this.tabPage_gps.ResumeLayout(false);
            this.tabPage_gps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CarLocationsPB)).EndInit();
            this.settingsGB.ResumeLayout(false);
            this.settingsGB.PerformLayout();
            this.tabPage_server.ResumeLayout(false);
            this.groupBox_server.ResumeLayout(false);
            this.groupBox_server.PerformLayout();
            this.groupBox_initiate.ResumeLayout(false);
            this.groupBox_initiate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer SerialTimer;
        private System.Windows.Forms.StatusStrip infoSS;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabControl_switchview;
        private System.Windows.Forms.TabPage tabPage_gps;
        private System.Windows.Forms.Label CarInfoLBL;
        private System.Windows.Forms.ListBox CarsLB;
        private System.Windows.Forms.Button ClearBTTN;
        private System.Windows.Forms.Button TestBTTN;
        private System.Windows.Forms.PictureBox CarLocationsPB;
        private System.Windows.Forms.GroupBox settingsGB;
        private System.Windows.Forms.Button closeConnectionBTTN;
        private System.Windows.Forms.ListBox SerialComLB;
        private System.Windows.Forms.Button ConnectBTTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox baudRateCBB;
        private System.Windows.Forms.Button refreshComPortBTTN;
        private System.Windows.Forms.ComboBox comPortCBB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage_server;
        private System.Windows.Forms.GroupBox groupBox_initiate;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Button button_initiate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numeric_port;
        private System.Windows.Forms.Label label_connection;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.GroupBox groupBox_server;
        private System.Windows.Forms.Button SetIpBTTN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox IpAddressTB;
    }
}

