using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PTT2CarGps.locationServer
{
    public class ESP
    {
        public TcpClient Client { get; private set; }
        public IPAddress Ip { get; private set; }
        public int Id { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public ESP(TcpClient client)
        {
            Client = client;
            Ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
            X = 0;
            Y = 0;
        }

        public void setXY(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
