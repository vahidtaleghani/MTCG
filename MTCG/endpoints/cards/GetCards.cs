using MTCG.helper;
using MTCG.repository;
using MTCG.repository.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MTCG.endpoints.cards
{
    public class GetCards : IEndpoint
    {
        struct Cardobject
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public double Damage { get; set; }
            public int package_id { get; set; }
            public string username { get; set; }
            public bool deck { get; set; }
        }

        public bool canProcrss(Request request)
        {
            return request.path.Equals("/cards")
                && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden("There is no token");

                List<Card> cardList = new CardReps().getAllCardsByUsername(username);
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
