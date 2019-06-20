using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace DP_firstLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            //string compName = Dns.GetHostName();
            //Console.WriteLine($"This hosy name: {compName}");
            //IPHostEntry ipList = Dns.GetHostByName(compName);

            //foreach (var ip in ipList.AddressList)
            //{
            //    Console.WriteLine(ip.ToString());
            //}

            //compName = Console.ReadLine();

            //ipList = Dns.GetHostByName(compName);

            //foreach (var ip in ipList.AddressList)
            //{
            //    Console.WriteLine(ip.ToString());
            //}
            //Console.ReadLine();

            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //socket.Connect(ipList.AddressList[0], 443);
            //try
            //{
            //    if (socket.Connected)
            //    {
            //        string httpRequest = "GET/HTTP1.0";
            //        socket.Send(Encoding.ASCII.GetBytes(httpRequest));
            //        var buffer = new byte[1024 * 4];
            //        socket.Receive(buffer);

            //        Console.WriteLine($" Recived: {Encoding.UTF8.GetString(buffer)}");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error");

            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("ERROR: " + ex.Message);
            //}

            IPAddress ip = IPAddress.Parse("0.0.0.0");
            var port = 12345;
            Server(ip, port); 
        }

        static void Server(IPAddress ipAddress, int port)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, port);
            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(100);
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("Client connected: ", clientSocket.RemoteEndPoint.ToString());
                try
                {
                    var buffer = new byte[1024];
                    int reciveSize = clientSocket.Receive(buffer);
                    Console.WriteLine($"Recived {reciveSize} bytes");
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                    var sendSize = clientSocket.Send(buffer, reciveSize, SocketFlags.None);
                    Console.WriteLine($"Send to client {sendSize} bytes");

                }
                catch(Exception exception)
                {
                    Console.WriteLine("Error: " + exception.Message);
                }
            }
        }
    }
}
