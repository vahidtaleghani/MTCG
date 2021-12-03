using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints
{
    public class HelloWorld : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/hello");
        }

        public Response handleRequest(Request request)
        {
            Response response = new Response(Response.StatusCode.OK, "hello");
            return response;
        }
    }
}
