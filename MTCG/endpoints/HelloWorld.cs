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
            Response response = new Response();
            return request.path.Equals("/hello") && 
                request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON]) == true;
        }

        public Response handleRequest(Request request)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, response.status_code_value[StatusCode.OK], response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
    }
}
