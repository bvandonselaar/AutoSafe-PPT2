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
        public int currentTalkIndex { get; private set; } = 0;

        public List<ESP> connectedESPs { get; private set; }
        public string IP { get; private set; }
        public int Port { get; private set; }

        public LocationServer()
        {
            socketListener = null;
            connectedESPs = new List<ESP>();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(TimerEventProcessor);
        }

        private async void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (socketListener.Pending())
            {
                AcceptConnection();
            }

            if (currentTalkIndex < connectedESPs.Count)
            {
                try
                {
                    StartTalkWith(currentTalkIndex);
                    await Receive();
                }
                catch (ArgumentOutOfRangeException)
                {
                    currentTalkIndex = -1;
                }
                catch (ObjectDisposedException)
                {
                    currentTalkIndex = -1;
                }
                finally
                {
                    currentTalkIndex++;
                    if (currentTalkIndex > connectedESPs.Count - 1)
                    {
                        currentTalkIndex = 0;
                    }
                }
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

        public void SendLocation(UInt16 x, UInt16 y)
        {
            byte xHigh = (byte)(0xFF & (x >> 8));
            byte xLow = (byte)(0xFF & x);

            byte yHigh = (byte)(0xFF & (y >> 8));
            byte yLow = (byte)(0xFF & y);
            byte[] payload = { xHigh, xLow, yHigh, yLow };

            Packet p = new Packet();
            byte[] message = p.Serialize(0xA2, 1, payload);
            tcpStream.Write(message, 0, message.Length);
        }

        public void setSearchingMode(bool mode)
        {
            if (mode)
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
            byte[] bytes = new byte[20];
            if (tcpStream.CanRead)
            {
                await tcpStream.ReadAsync(bytes, 0, bytes.Length);
                if (bytes[0] == 0x0E && bytes[1] == 0xE0)
                {
                    int id = (bytes[5] << 8) | bytes[6];
                    int x = 0;
                    int y = 0;

                    x = connectedESPs[currentTalkIndex].X;
                    y = connectedESPs[currentTalkIndex].Y;

                    SendLocation((UInt16)x, (UInt16)y);
                }
                foreach (byte b in bytes)
                {
                    if (b != 0)
                    {
                        count = 0;
                        return bytes;
                    }
                }
                count++;
                if (count > 5)
                {
                    tcpStream.Close();
                    connectedESPs.RemoveAt(currentTalkIndex);
                    currentTalkIndex = connectedESPs.Count;
                }
            }
            return bytes;
        }


    }
}
