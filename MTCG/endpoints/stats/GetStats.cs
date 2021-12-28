using MTCG.data.entity;
using MTCG.helper;
using MTCG.repository;
using MTCG.repository.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.stats
{
    public class GetStats : IEndpoint
    {

        public bool canProcrss(Request request)
        {
            return request.path.Equals("/stats")
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
                    StatsUser statUser = new StatReps().getStatsUserByUsername(username);
                    //User user = new UserReps().getUser(username);
                    if (statUser == null)
                        return ResponseCreator.serverError("Username in stat table not found");
                    String jsonString = JsonConvert.SerializeObject(statUser);
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
