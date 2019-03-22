using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Oving4Udp 
{
    public struct Received 
    {
        public IPEndPoint Sender;
        public string Message;
    }

    public class UdpBase 
    {
        protected UdpClient Client;

        protected UdpBase() {
            Client = new UdpClient();
        }
        
        public async Task<Received> Receive() {
            var result = await Client.ReceiveAsync();

            return new Received()
            {
                Message = Encoding.UTF8.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }
}