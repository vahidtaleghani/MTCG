using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static MTCG.Response;

namespace MTCG.endpoints.users
{
    public class PostUsers : IEndpoint
    {
        private Userobject reqUsers;

        struct Userobject
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public bool canProcrss(Request request)
        {
            Response response = new Response();
            try
            {
                return request.path.Equals("/users")
                    && request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON]) == true
                    && request.getMethode().Equals(Request.METHODE.POST);
            }
            catch (Exception)
            {
                return false;
            }
      
        }

        public Response handleRequest(Request request)
        {
            try
            {
                this.reqUsers = JsonConvert.DeserializeObject<Userobject>(request.getPayload());
                if(String.IsNullOrEmpty(this.reqUsers.Username) || String.IsNullOrEmpty(this.reqUsers.Password))
                    return ResponseCreator.jsonInvalid();
                if(!new UserReps().addUser(this.reqUsers.Username, this.reqUsers.Password))
                    return ResponseCreator.jsonInvalid("username is existiert");
                Console.WriteLine("  User with name : " + this.reqUsers.Username + " and password: " + this.reqUsers.Password + " added");
            }
            catch (Exception)
            {
                return ResponseCreator.jsonInvalid();
            }
            return ResponseCreator.ok();
        }
    
    }
}
