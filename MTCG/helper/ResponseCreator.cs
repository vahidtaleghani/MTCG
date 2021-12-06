using System;
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

        public static Response ok(String message)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }

        public static Response jsonInvalid()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Bad_Request, response.status_code_value[StatusCode.Bad_Request], response.Content_Type_value[Content_Type.HTML]);
            return response;
        }

        public static Response jsonInvalid(String message)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Bad_Request, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }

        public static Response forbidden(String message)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.forbidden, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
    }
}
