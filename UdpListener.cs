using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Oving4Udp
{
    public class UdpListener : UdpBase
    {
        private IPEndPoint _listenOn;

        public UdpListener() : this(new IPEndPoint(IPAddress.Any, 321123)) { }

        public UdpListener(IPEndPoint endpoint) 
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }

        public void Reply(string message, IPEndPoint endpoint)  
        {
            var datagram = Encoding.UTF8.GetBytes(message);
            Client.Send(datagram, datagram.Length, endpoint);
        }
    }
}