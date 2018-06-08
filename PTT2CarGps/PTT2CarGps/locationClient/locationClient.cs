using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PTT2CarGps.locationClient
{
    public class LocationClient
    {
        private TcpClient locClient;
        private NetworkStream tcpStream;

        public byte[] Bytes { get; private set; }
        public string IP { get; private set; }
        public int Port { get; private set; }


        public LocationClient()
        {
            locClient = new TcpClient();
            IP = "0";
            Port = 0;
            Bytes = new byte[20];
        }

        public void SendLocation(byte id, UInt16 x, UInt16 y)
        {
            byte xHigh = (byte)(0xFF & (x >> 8));
            byte xLow = (byte)(0xFF & x);

            byte yHigh = (byte)(0xFF & (y >> 8));
            byte yLow = (byte)(0xFF & y);
            byte[] payload = { xHigh, xLow, yHigh, yLow };

            Packet p = new Packet();
            byte[] message = p.Serialize(0xA2, payload);
            tcpStream.Write(message, 0, message.Length);
        }

        public void SendWarning(byte id)
        {
            Packet p = new Packet();
            byte[] message = p.Serialize(0xC8, null);
            tcpStream.Write(message, 0, message.Length);
        }

        public void SendEmergencyWarning(byte id)
        {
            Packet p = new Packet();
            byte[] message = p.Serialize(0xC9, null);
            tcpStream.Write(message, 0, message.Length);
        }

        public async Task Connect(string ip, int port)
        {
            if (locClient.Connected) { locClient.Close(); }

            IP = ip;
            Port = port;
            locClient = new TcpClient();

            await locClient.ConnectAsync(IPAddress.Parse(ip), port);
            tcpStream = locClient.GetStream();
        }

        public void Disconnect()
        {
            IP = "0";
            Port = 0;
            locClient.Close();
        }

        public async Task Read()
        {
            await tcpStream.ReadAsync(Bytes, 0, Bytes.Length);
        }

        public override string ToString()
        {
            try
            {
                return "Connected: " + locClient.Connected.ToString() +
                        "\nIP: " + IP +
                        "\nPort: " + Port;
            }
            catch
            {
                return "No TcpClient initiated";
            }
        }
    }
}
