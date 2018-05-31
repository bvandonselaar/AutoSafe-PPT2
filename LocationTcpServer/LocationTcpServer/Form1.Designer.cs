namespace LocationTcpServer
{
    partial class locationServerForm
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
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numeric_port = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button_initiate = new System.Windows.Forms.Button();
            this.groupBox_initiate = new System.Windows.Forms.GroupBox();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.numeric_choose = new System.Windows.Forms.NumericUpDown();
            this.button_choose = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_acceptConnections = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_port)).BeginInit();
            this.groupBox_initiate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_choose)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(45, 19);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 20);
            this.textBox_ip.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP:";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // button_initiate
            // 
            this.button_initiate.Location = new System.Drawing.Point(45, 72);
            this.button_initiate.Name = "button_initiate";
            this.button_initiate.Size = new System.Drawing.Size(100, 23);
            this.button_initiate.TabIndex = 4;
            this.button_initiate.Text = "Initiate";
            this.button_initiate.UseVisualStyleBackColor = true;
            this.button_initiate.Click += new System.EventHandler(this.button_initiate_Click);
            // 
            // groupBox_initiate
            // 
            this.groupBox_initiate.Controls.Add(this.textBox_ip);
            this.groupBox_initiate.Controls.Add(this.button_initiate);
            this.groupBox_initiate.Controls.Add(this.label1);
            this.groupBox_initiate.Controls.Add(this.label2);
            this.groupBox_initiate.Controls.Add(this.numeric_port);
            this.groupBox_initiate.Location = new System.Drawing.Point(12, 12);
            this.groupBox_initiate.Name = "groupBox_initiate";
            this.groupBox_initiate.Size = new System.Drawing.Size(168, 105);
            this.groupBox_initiate.TabIndex = 5;
            this.groupBox_initiate.TabStop = false;
            this.groupBox_initiate.Text = "Initiate";
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(12, 123);
            this.textBox_input.Multiline = true;
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.ReadOnly = true;
            this.textBox_input.Size = new System.Drawing.Size(423, 211);
            this.textBox_input.TabIndex = 8;
            // 
            // numeric_choose
            // 
            this.numeric_choose.Location = new System.Drawing.Point(384, 27);
            this.numeric_choose.Name = "numeric_choose";
            this.numeric_choose.Size = new System.Drawing.Size(138, 20);
            this.numeric_choose.TabIndex = 9;
            // 
            // button_choose
            // 
            this.button_choose.Location = new System.Drawing.Point(384, 52);
            this.button_choose.Name = "button_choose";
            this.button_choose.Size = new System.Drawing.Size(138, 23);
            this.button_choose.TabIndex = 10;
            this.button_choose.Text = "Choose";
            this.button_choose.UseVisualStyleBackColor = true;
            this.button_choose.Click += new System.EventHandler(this.button_choose_Click);
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(475, 172);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 11;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_acceptConnections
            // 
            this.checkBox_acceptConnections.AutoSize = true;
            this.checkBox_acceptConnections.Location = new System.Drawing.Point(186, 100);
            this.checkBox_acceptConnections.Name = "checkBox_acceptConnections";
            this.checkBox_acceptConnections.Size = new System.Drawing.Size(166, 17);
            this.checkBox_acceptConnections.TabIndex = 12;
            this.checkBox_acceptConnections.Text = "Accept incoming connections";
            this.checkBox_acceptConnections.UseVisualStyleBackColor = true;
            this.checkBox_acceptConnections.CheckedChanged += new System.EventHandler(this.checkBox_acceptConnections_CheckedChanged);
            // 
            // locationServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 386);
            this.Controls.Add(this.checkBox_acceptConnections);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.button_choose);
            this.Controls.Add(this.numeric_choose);
            this.Controls.Add(this.textBox_input);
            this.Controls.Add(this.groupBox_initiate);
            this.Name = "locationServerForm";
            this.Text = "LocationServer";
            ((System.ComponentModel.ISupportInitialize)(this.numeric_port)).EndInit();
            this.groupBox_initiate.ResumeLayout(false);
            this.groupBox_initiate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_choose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numeric_port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_initiate;
        private System.Windows.Forms.GroupBox groupBox_initiate;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.NumericUpDown numeric_choose;
        private System.Windows.Forms.Button button_choose;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.CheckBox checkBox_acceptConnections;
    }
}

