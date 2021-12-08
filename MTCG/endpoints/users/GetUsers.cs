using MTCG.helper;
using MTCG.repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MTCG.Response;

namespace MTCG.endpoints.users
{
    public class GetUsers : IEndpoint
    {

        private String pattern = "/users/";
        public bool canProcrss(Request request)
        {
            Response response = new Response();
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
                if (substrings[0] == null)
                    return ResponseCreator.notFound();

                if (!user.Equals(substrings[1]))
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
