using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai_1_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 995);
            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;
            Console.WriteLine("Ready to receive…");
            while (true)
            {

                if (sock.Poll(10000, SelectMode.SelectRead))
                {
                    byte[] data = new byte[1024];
                    int recv = sock.ReceiveFrom(data, ref ep);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine("received: {0} from: {1}", stringData, ep.ToString());

                    data = new byte[1024];
                    string strs = "avaible";
                    data = Encoding.ASCII.GetBytes(strs);
                    sock.SendTo(data, ep);
                }
            }

            sock.Close();
        }
    }
}
