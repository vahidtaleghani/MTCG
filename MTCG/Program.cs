using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MTCG
{
    class Program
    {

        private static int PORT = 10001;
        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Loopback, PORT);
                listener.Start(5);

                while (true)
                {
                    Console.WriteLine("Server Started\n Listening for Connection on Port "+ PORT+"\n");
                    var socket = listener.AcceptTcpClient();
                    HTTPHandler httphandler = new HTTPHandler(socket);
                    Thread thread = new Thread(httphandler.Process);
                    thread.Start();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
         
        }
    }
}
