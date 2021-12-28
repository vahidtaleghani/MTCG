using MTCG.helper;
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
                    return ResponseCreator.forbidden();

                String[] substrings = Regex.Split(request.path, this.pattern);
                if (substrings[0] == null || !user.Equals(substrings[1]))
                    return ResponseCreator.notFound();
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
            return ResponseCreator.ok();
        }
    }
}
