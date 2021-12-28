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
            return request.path.Equals("/battle")
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                // kontrollieren Headers
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden("This user does not exist");
                
                // kontrollieren, ob users hat 4 karte
                
                if(new CardReps().getAllCardInDeckByUsername(username).Count !=4)
                    return ResponseCreator.forbidden("This user does not have 4 Card in Deck");

                // Kontrolliert, dass der user nur einmal angefordert hat
                if (!BattleController.getInstance().addPlayer(username))
                    return ResponseCreator.badRequest("You are already in a Tournament");
        
                while (BattleController.getInstance().isInProgress())
                {
                    Thread.Sleep(100);
                    //return ResponseCreator.okJsonPayload(BattleController.getInstance().getLastResult());
                }

                // Tournament ist fertig und detailliertes Protokoll erhalten und zurückgeben

                return ResponseCreator.forbidden("no user");

            }
            catch (Exception)
            {
                return ResponseCreator.serverError();
            }
           
        }
    }
}
