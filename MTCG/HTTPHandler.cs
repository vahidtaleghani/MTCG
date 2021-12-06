using MTCG.endpoints;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class HTTPHandler
    {
        private TcpClient socket;

        public HTTPHandler(TcpClient socket)
        {
            this.socket = socket;
        }
        public void Process()
        {

            TcpClient client = socket;
            try
            {
                Console.WriteLine("| Connection opened");

                Request request = new Request(new StreamReader(client.GetStream()));

                Response response = Program.endpointController.getResponse(request);

                response.Send(new StreamWriter(client.GetStream()));

                Console.WriteLine("| Connection closed");

                Console.WriteLine("----------------------------------------");


            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                Console.WriteLine("Closing client connection");
            }
            
        }
    }
}
