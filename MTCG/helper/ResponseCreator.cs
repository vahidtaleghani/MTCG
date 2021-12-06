using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTCG.Response;

namespace MTCG.helper
{
    public class ResponseCreator
    {
        public static Response notFound()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Not_Found, response.status_code_value[StatusCode.Not_Found], response.Content_Type_value[Content_Type.HTML]);
            return response;
        }

        public static Response ok()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, response.status_code_value[StatusCode.OK], response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
    }
}
