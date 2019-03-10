using System.Text;

namespace Oving4Udp
{
    public class UdpUser : UdpBase
    {
        private UdpUser() {}

        public static UdpUser ConnectTo(string hostname, int port) 
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message) 
        {
            var datagram = Encoding.UTF8.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}