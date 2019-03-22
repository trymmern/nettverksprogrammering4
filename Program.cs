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
            Task.Factory.StartNew(async () => {
                while (true) {
                    var num1 = await server.Receive();
                    Console.WriteLine($"First number: {num1.Message}");
                    var op = await server.Receive();
                    Console.WriteLine($"operand: {op.Message}");
                    var num2 = await server.Receive();
                    Console.WriteLine($"Second number: {num2.Message}");

                    if (op.Message == "+") {
                        server.Reply($"{num1.Message}+{num2.Message}={Int32.Parse(num1.Message)+Int32.Parse(num2.Message)}", num1.Sender);
                        
                    }
                    else if (op.Message == "-") {
                        server.Reply($"{num1.Message}-{num2.Message}={Int32.Parse(num1.Message)-Int32.Parse(num2.Message)}", num1.Sender);
                        
                    }
                    else {
                        server.Reply($"{op.Message} is not a valid operand", num1.Sender);
                    }

                    //server.Reply($"Copy {received.Message}", received.Sender);
                    if (num1.Message == "exit" || op.Message == "exit" || num2.Message == "exit")
                        break;
                }
            });

            // Create a new client
            var client = UdpUser.ConnectTo("127.0.0.1", 8080);

            // Wait for reply messages from server and send them to console
            Task.Factory.StartNew(async () => {
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
