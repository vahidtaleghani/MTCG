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
            return request.path.Equals("/users") 
                && request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON]) == true 
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                this.reqUsers = JsonConvert.DeserializeObject<Userobject>(request.getPayload());

                if (this.reqUsers.Username == null || this.reqUsers.Password == null)
                    return ResponseCreator.jsonInvalid();
                new UserReps().addUser(this.reqUsers.Username, this.reqUsers.Password);
                //List<User> userlists = new UserReps().getAllUsers();
                //foreach (User users in userlists)
                //{
                //   Console.WriteLine((users == null) ? "Not Found" : users.toStringUser());
                //}
                Console.WriteLine("User with name : " + this.reqUsers.Username + " and password: " + this.reqUsers.Password + " added");
               
            }
            catch (Exception)
            {
                return ResponseCreator.jsonInvalid();
            }
            
            return ResponseCreator.ok();
        }
    
    }
}
