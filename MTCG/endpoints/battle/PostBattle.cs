using MTCG.helper;
using MTCG.repository;
using System;
using System.Threading;

namespace MTCG.endpoints.battle
{
    public class PostBattle : IEndpoint
    {
        
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/battles")
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                // kontrollieren Headers
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");

                // kontrollieren, ob users hat 4 karte
                if (new CardReps().getDeckByUsername(username).Count !=4)
                    return ResponseCreator.forbidden("This user does not have 4 Card in Deck");

                // Kontrolliert, dass der user nur einmal angefordert hat
                if (!Battle.getInstance().addPlayer(username))
                    return ResponseCreator.badRequest("This user already in a Tournament");
        
                while (Battle.getInstance().isInProgress())
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("| Error: " + exc.Message);
                    }
                }

                if(Battle.getInstance().isPlayed())
                    return ResponseCreator.okJsonPayload(Battle.getInstance().getLastResult());
                return ResponseCreator.forbidden("No User ready to play");

            }
            catch (Exception)
            {
                return ResponseCreator.serverError();
            }
           
        }
    }
}
