using System;
using System.Collections.Generic;
using System.IO;


namespace MTCG
{
    public class Response
    {
        public enum StatusCode { OK , Bad_Request , Not_Found , Forbidden, Internal_Server_Error }
        public enum Content_Type { PLAIN, JSON, HTML }

        public Dictionary<StatusCode, string> status_code_value { get; set; }
        public Dictionary<Content_Type, string> Content_Type_value { get; set; }

        public StatusCode statusCode { get; private set; }
        public String payload { get; private set; }
        private String contentType;

        public Response() 
        {
               status_code_value = new Dictionary<StatusCode, string>()
               {
                   { StatusCode.OK, "200 OK" },
                   { StatusCode.Bad_Request, "400 Bad Request" },
                   { StatusCode.Not_Found, "404 Not Found" },
                   { StatusCode.Forbidden, "403 Forbidden" },
                   { StatusCode.Internal_Server_Error, "500 Internal Server Error" }
               };

                Content_Type_value = new Dictionary<Content_Type, string>()
                {
                    { Content_Type.JSON, "application/json" },
                    { Content_Type.HTML, "text/html" },
                    { Content_Type.PLAIN, "text/plain" }

                };
        }
        private bool isValid()
        {
            return this.statusCode != null && (this.payload == null || this.contentType != null);
        }
        public void Send(StatusCode statusCode, String payload, String contentType)
        {
            //--
            this.statusCode = statusCode;
            this.payload = payload;
            this.contentType = contentType;
        }
        public void Send(StreamWriter sw)
        {
            if(!isValid())
                throw new Exception("Response not full created");

            string sc = status_code_value[this.statusCode];
            StreamWriter streamwriter = sw;
          
            try
            {
                streamwriter.WriteLine($"HTTP/1.1 "+ sc);
                streamwriter.WriteLine("Server: C# HTTP Server for SWE1: 1.0");
                streamwriter.WriteLine("Date: " + DateTime.Now);
                streamwriter.WriteLine("Content-Type: "+ contentType);

                if (!string.IsNullOrEmpty(this.payload))
                {
                    streamwriter.WriteLine("Content-Lenght: " + this.payload.Length);
                    streamwriter.WriteLine();
                    streamwriter.WriteLine(this.payload);
                }
                else
                {
                    streamwriter.WriteLine("Content-Lenght: 0");
                    streamwriter.WriteLine();
                }

                streamwriter.Flush();
                streamwriter.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
            }

        }

    }

}
