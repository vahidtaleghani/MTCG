using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using static MTCG.Response;

namespace MTCG.endpoints.session
{
    public class PostSession : IEndpoint
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
            return request.path.Equals("/sessions")
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                this.reqUsers = JsonConvert.DeserializeObject<Userobject>(request.getPayload());
                if (String.IsNullOrEmpty(this.reqUsers.Username) || String.IsNullOrEmpty(this.reqUsers.Password))
                    return ResponseCreator.jsonInvalid();
                String result = new UserReps().getToken(this.reqUsers.Username, this.reqUsers.Password);

                if(result == null)
                    return ResponseCreator.jsonInvalid();
                if (result.Equals("Password is Wrong"))
                    return ResponseCreator.forbidden(result);
                Console.WriteLine("  User with name : " + this.reqUsers.Username + " and token: " + result + " is loggedin");
            }
            catch (Exception)
            {
                return ResponseCreator.jsonInvalid();
            }

            return ResponseCreator.ok("You are logged in");
        }
    }
}
