using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bai_1_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 995);
            EndPoint ep = (EndPoint)iep1;
            string hostname = Dns.GetHostName();

            while (true)
            {
                byte[] data = Encoding.ASCII.GetBytes(hostname);
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                sock.SendTo(data, iep1);
                Thread listen = new Thread(() =>
                {
                    while (true)
                    {
                        data = new byte[1024];
                        int recv = sock.ReceiveFrom(data, ref ep);
                        string stringData = Encoding.ASCII.GetString(data, 0, recv);
                        Console.WriteLine("received: {0} from: {1}", stringData, ep.ToString());
                    }
                });
                listen.Start();
                Thread.Sleep(10000);
            }
            sock.Close();
        }
    }
}
