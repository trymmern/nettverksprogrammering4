using System;
using System.Text;

namespace Oving4Udp
{
    public class UdpUser : UdpBase
    {
        public UdpUser() {}

        public static UdpUser ConnectTo(string hostname, int port) 
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message) 
        {
            Console.WriteLine($"Sending {message} from client...\n");
            var datagram = Encoding.UTF8.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}