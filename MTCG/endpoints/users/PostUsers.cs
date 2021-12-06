using MTCG.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTCG.Response;

namespace MTCG.endpoints.users
{
    public class PostUsers : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            Response response = new Response();
            return request.path.Equals("/users") 
                && request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON]) == true 
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            Console.WriteLine(request.getPayload());
            return ResponseCreator.ok();
        }


    }
}
