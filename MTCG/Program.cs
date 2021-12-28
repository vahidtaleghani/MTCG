using MTCG.Play;
using MTCG.repository;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MTCG
{
    class Program
    {
     
        private static int PORT = 10001;
        public static EndpointController endpointController = new EndpointController();
        public static FightController fightController = new FightController();
        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Loopback, PORT);
                listener.Start(5);

                while (true)
                {
                    Console.WriteLine("Server Started");
                    //connected to database
                    Database.GetInstance();
                    Console.WriteLine("Listening for Connection on Port: " + PORT);
                    Console.WriteLine("----------------------------------------");

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
