using MTCG.data.entity;
using MTCG.data.repository;
using MTCG.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.trade
{
    public class GetTrade : IEndpoint

    {
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/tradings")
               && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");
                try
                {
                    List<Trade> trade = new TradeReps().getAllCardInStore();

                    if (trade == null)
                        return ResponseCreator.serverError("Store is empty");
                    String jsonString = JsonConvert.SerializeObject(trade);
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
