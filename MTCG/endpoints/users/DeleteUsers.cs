using MTCG.helper;
using MTCG.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTCG.endpoints.users
{
    public class DeleteUsers : IEndpoint
    {
        private String pattern = "/users/";
        public bool canProcrss(Request request)
        {
            return Regex.IsMatch(request.path, "/users/([0-9a-zA-Z.-]+)")
                && request.getMethode().Equals(Request.METHODE.DELETE);
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
                    return ResponseCreator.forbidden("You are not authorized to delete user");

                if (!new CardReps().deleteCards(user) || !new UserReps().deleteUser(user))
                    return ResponseCreator.serverError();
                return ResponseCreator.ok();
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
        }
    }
}
