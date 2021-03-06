using MTCG.helper;
using System;
using System.IO;
using System.Net.Sockets;


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

                Response response;
                try
                {
                    response = Program.endpointController.getResponse(request);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("| Error: " + exc.Message);
                    response = ResponseCreator.serverError();
                }
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
