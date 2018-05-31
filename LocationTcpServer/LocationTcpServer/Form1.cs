using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationTcpServer
{
    public partial class locationServerForm : Form
    {
        LocationServer server;
        public delegate void InvokeDelegate();

        public locationServerForm()
        {
            InitializeComponent();
            server = new LocationServer();
        }

        private void button_initiate_Click(object sender, EventArgs e)
        {
            string ip = textBox_ip.Text;
            int port = (int)numeric_port.Value;
            server.InitiateListener(ip, port);
            groupBox_initiate.Enabled = false;
        }

        private async void button_choose_Click(object sender, EventArgs e)
        {
            bool loop = true;
            server.StartTalkWith((int)numeric_choose.Value);
            MessageBox.Show(server.connectedESPs[(int)numeric_choose.Value].Ip.ToString());
            while (loop)
            {
                try
                {
                    byte[] bytes = await server.Receive();
                    foreach (byte b in bytes)
                    {
                        textBox_input.AppendText(b.ToString());
                    }
                    textBox_input.AppendText("\n");
                }
                catch(ObjectDisposedException)
                {
                    loop = false;
                    MessageBox.Show("Disconnected");
                }
                
                
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            server.Send("hoi");
        }

        private void checkBox_acceptConnections_CheckedChanged(object sender, EventArgs e)
        {
            server.setSearchingMode(checkBox_acceptConnections.Checked);
        }










        /*
        public void Receive()
        {
            buffer = new byte[30];
            server.TcpStream.BeginRead(buffer, 0, buffer.Length, ReadComplete, buffer); 
        }

        private void ReadComplete(IAsyncResult iar)
        {
            buffer = (byte[])iar.AsyncState;
            int bytesAvailable = server.TcpStream.EndRead(iar);

            textBox_input.BeginInvoke(new InvokeDelegate(writeText));
            server.TcpStream.BeginRead(buffer, 0, buffer.Length, ReadComplete, buffer);    
        }

        private void writeText()
        {
            foreach (byte b in buffer)
            {
                textBox_input.AppendText(b.ToString());
            }
            textBox_input.AppendText("\n");
        }
        */
    }
}
