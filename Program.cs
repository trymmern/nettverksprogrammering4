using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Oving4Udp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create a new server
            var server = new UdpListener();

            // start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async => {
                while (true) {
                    var received = await server.Receive();
                    server.Reply($"Copy {received.Message}", received.Sender);
                    if (received.Message == "exit")
                        break;
                }
            });

            // Create a new client
            var client = new UdpUser.ConnectTo("127.0.0.1", 32123);

            // Wait for reply messages from server and send them to console
            Task.Factory.StartNew(async => {
                while (true) {
                    try {
                        var received = await client.Receive();
                        Console.WriteLine(received.Message);
                        if (received.Message.Contains("exit"))
                            break;
                    }
                    catch (Exception e) 
                    {
                        Debug.Write(e);
                    }
                }
            });

            // Type something
            string read;
            do 
            {
                read = Console.ReadLine();
                client.Send(read);
            } while (read != "exit");
        }
    }
}
