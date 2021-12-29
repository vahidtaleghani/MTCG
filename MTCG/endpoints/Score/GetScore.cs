using MTCG.data.entity;
using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.Score
{
    public class GetScore : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/score")
                && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden("There is no token");
                try
                {
                    List<StatsUser> listStatUser = new StatReps().getStatsAllUsers();
                    if (listStatUser.Count == 0)
                        return ResponseCreator.serverError("No Users in stat table found");
                    String jsonString = JsonConvert.SerializeObject(listStatUser);
                    return ResponseCreator.okJsonPayload(jsonString);
                }
                catch (Exception)
                {
                    return ResponseCreator.forbidden();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                return ResponseCreator.forbidden();
            }
        }
    }
}
