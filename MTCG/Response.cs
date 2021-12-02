using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Response
    {
        public enum StatusCode { OK , Bad_Request , Not_Found , Forbideen, Internal_Server_Error }
        public enum Content_Type { PLAIN, JSON, HTML }

        public readonly Dictionary<StatusCode, string> status_code_value = new()
        {
            { StatusCode.OK, "200 OK" },
            { StatusCode.Bad_Request, "400 Bad Request" },
            { StatusCode.Not_Found, "404 Not Found" },
            { StatusCode.Forbideen, "403 Forbidden" },
            { StatusCode.Internal_Server_Error, "500 Internal Server Error" }
        };

        public readonly Dictionary<Content_Type, string> Content_Type_value = new()
        {
            { Content_Type.JSON, "application/json" },
            { Content_Type.HTML, "text/html" },
            { Content_Type.PLAIN, "text/plain" }

        };

        private StatusCode statusCode;
        private String payload;
        private String contentType;
        public StatusCode statuscode
        {
            set
            {
                statusCode = statusCode;
            }
        }
        public String Payload 
        {
            set 
            { 
                payload = payload; 
            }  
        }

        /*
        public String contentType
        {
            set
            {
                contentType = Content_Type_value[Content_Type.PLAIN];
            }
        }*/

        public Response(StatusCode statusCode, String payload) 
        {
            //????
            this.statusCode = statusCode;
            this.payload = payload;
            this.contentType = Content_Type_value[Content_Type.HTML];
        }
        public void Send(StreamWriter sw)
        {
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
