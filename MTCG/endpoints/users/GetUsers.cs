using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;


namespace MTCG.endpoints.users
{
    public class GetUsers : IEndpoint
    {

        private String pattern = "/users/";
        public bool canProcrss(Request request)
        {
            return Regex.IsMatch(request.path, "/users/([0-9a-zA-Z.-]+)")
                && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String user = new Authorize().authorizeUser(request);
                if (user == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");

                String[] substrings = Regex.Split(request.path, this.pattern);
                if (substrings[0] == null || !user.Equals(substrings[1]))
                    return ResponseCreator.forbidden("You are not authorized to view this profile");

                User userInfo = new UserReps().getUser(user);
                String jsonString = JsonConvert.SerializeObject(userInfo);
                return ResponseCreator.okJsonPayload(jsonString);
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
        }
    }
}
