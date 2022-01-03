using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static MTCG.Response;

namespace MTCG.endpoints.cards
{
    public class PutDeck : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            Response response = new Response();
            return request.path.Equals("/deck")
                && request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON])
                && request.getMethode().Equals(Request.METHODE.PUT);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");
                // Kontrolieren Json
                try
                {
                    var reqDecks = JsonConvert.DeserializeObject<List<String>>(request.getPayload());
                    
                    if (reqDecks.Count == 0)
                        return ResponseCreator.jsonInvalid();

                    List<String> response = new List<String>();
                    // Kontrollieren die Anzahl der ausgewählten Karten
                    if (reqDecks.Count != 4)
                        return ResponseCreator.forbidden("You must select 4 cards");

                    foreach (var reqDeck in reqDecks)
                    {
                        if (String.IsNullOrEmpty(reqDeck))
                            response.Add("No Card selected ");
                        else
                        {
                            if (!new CardReps().ControlCardByUsername(reqDeck, username))
                                response.Add(reqDeck + ": card not exist or not belong to user or already selected");
                        }      
                    }
                    if (response.Count > 0)
                    {
                        String responseString = JsonConvert.SerializeObject(response);
                        return ResponseCreator.forbidden(responseString);
                    }
                    foreach (var reqDeck in reqDecks)
                    {
                        if (!new CardReps().updateDeckByUsername(reqDeck, username))
                            return ResponseCreator.serverError("Server Error: Card could not add to deck");
                    }
                    
                }
                catch (Exception)
                {
                    return ResponseCreator.jsonInvalid();
                }
                return ResponseCreator.ok("All Card added to deck");
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
        }
    }
}
