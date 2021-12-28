using MTCG.helper;
using MTCG.repository;
using MTCG.repository.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.deck
{
    public class GetDeck : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/deck")
                && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden("There is no token");

                List<Card> cardList = new CardReps().getDeckByUsername(username);
                if(cardList.Count == 0)
                    return ResponseCreator.ok("The deck is not configured");

                String jsonString = JsonConvert.SerializeObject(cardList);
                return ResponseCreator.okJsonPayload(jsonString);
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
        }
    }
}
