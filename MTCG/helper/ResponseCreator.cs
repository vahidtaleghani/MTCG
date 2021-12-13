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
            response.Send(Response.StatusCode.Forbidden, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
        public static Response forbidden()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Forbidden, response.status_code_value[StatusCode.Forbidden], response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
        public static Response badRequest(String message)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Bad_Request, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
        public static Response okJsonPayload(String jsonString)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, jsonString, response.Content_Type_value[Content_Type.JSON]);
            return response;
        }
        public static Response serverError(String message)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Internal_Server_Error, message, response.Content_Type_value[Content_Type.HTML]);
            return response;
        }
    }
}
