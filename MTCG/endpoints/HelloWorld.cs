using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTCG.Response;

namespace MTCG.endpoints
{
    public class HelloWord : IEndpoint
    {
        public bool canProcrss(Request request)
        {
       
            return request.path.Equals("/hello");
        }

        public Response handleRequest(Request request)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, response.status_code_value[StatusCode.OK]);
            return response;
        }
    }
}
