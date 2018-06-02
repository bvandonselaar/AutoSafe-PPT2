using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PTT2CarGps.locationServer
{
    public class LocationServer
    {
        private TcpListener socketListener;
        private NetworkStream tcpStream;
        private System.Windows.Forms.Timer timer;
        private int currentTalkIndex = 0;
      
        public List<ESP> connectedESPs { get; private set; }
        public string IP { get; private set; }
        public int Port { get; private set; }

        public LocationServer()
        {
            socketListener = null;
            connectedESPs = new List<ESP>();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000;
            timer.Tick += new EventHandler(TimerEventProcessor);
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if(socketListener.Pending())
            {
                AcceptConnection();
            }

            if (currentTalkIndex < connectedESPs.Count)
            {
                StartTalkWith(currentTalkIndex);
                
            }
            
            currentTalkIndex++;
            if(currentTalkIndex > connectedESPs.Count)
            {
                currentTalkIndex = 0;
            }
            
        }

        //Server stappenplan
        //---------------------------------------------------------------------------------------

        //Stap 1: server start met luisteren naar inkomende connecties
        public void InitiateListener(string ip, int port)
        {
            IP = ip;
            Port = port;
            socketListener = new TcpListener(IPAddress.Parse(IP), Port);
            socketListener.Start();
        }
        public void InitiateListener()
        {
            socketListener = new TcpListener(IPAddress.Parse(IP), Port);
            socketListener.Start();
        }

        //Stap 2: server accepteert een inkomend connectie-verzoek
        public void AcceptConnection()
        {
            ESP e = new ESP(socketListener.AcceptTcpClient());
            connectedESPs.Add(e);
        }

        //Stap 3: server kiest met welke connectie die ook echt wil praten
        public void StartTalkWith(int index)
        {
            currentTalkIndex = index;
            tcpStream = connectedESPs[currentTalkIndex].Client.GetStream();
        }
        //---------------------------------------------------------------------------------------

        
        public void Send(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            tcpStream.Write(data, 0, data.Length);
        }

        public void SendLocation(byte id, UInt16 x, UInt16 y)
        {
            byte xHigh = (byte)(0xFF & (x >> 8));
            byte xLow = (byte)(0xFF & x);

            byte yHigh = (byte)(0xFF & (y >> 8));
            byte yLow = (byte)(0xFF & y);
            byte[] payload = { id, xHigh, xLow, yHigh, yLow };

            Packet p = new Packet();
            byte[] message = p.Serialize(0xA2, payload);
            tcpStream.Write(message, 0, message.Length);
        }

        public void setSearchingMode(bool mode)
        {
            if(mode)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        int count = 0;
        public async Task<byte[]> Receive()
        {
            byte[] bytes = new byte[30];
            await tcpStream.ReadAsync(bytes, 0, bytes.Length);
            foreach(byte b in bytes)
            {
                if(b != 0)
                {
                    count = 0;
                    return bytes;
                }
            }
            count++;
            if (count > 5)
            {
                tcpStream.Close();
                connectedESPs.RemoveRange(currentTalkIndex, 1);
            }
            return bytes; 
        }
    }
}
