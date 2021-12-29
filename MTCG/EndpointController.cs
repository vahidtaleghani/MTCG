using MTCG.endpoints;
using MTCG.endpoints.battle;
using MTCG.endpoints.cards;
using MTCG.endpoints.deck;
using MTCG.endpoints.packages;
using MTCG.endpoints.Score;
using MTCG.endpoints.session;
using MTCG.endpoints.stats;
using MTCG.endpoints.transactions;
using MTCG.endpoints.users;
using MTCG.helper;
using System.Collections.Generic;
using static MTCG.Response;

namespace MTCG
{
    public class EndpointController
    {
        List<IEndpoint> endpointList = new List<IEndpoint>();

        public EndpointController()
        {
            endpointList.Add(new PostUsers());
            endpointList.Add(new PostSession());
            endpointList.Add(new GetUsers());
            endpointList.Add(new PutUsers());
            endpointList.Add(new PostPackages());
            endpointList.Add(new PostTransactions());
            endpointList.Add(new GetCards());
            endpointList.Add(new GetDeck());
            endpointList.Add(new PutDeck());
            endpointList.Add(new GetDeckFormat());
            endpointList.Add(new GetStats());
            endpointList.Add(new GetScore());
            endpointList.Add(new PostBattle());
        }

        public Response getResponse(Request request)
        {
            // Suche den Endpunkte, welche die Anfrage bearbeiten kann
            // und lass diese Anfrage bearbeiten.
            // Gebe den Response züruck, den der Endpunkt generiert hat
            
            foreach (IEndpoint endpoint in endpointList)
            {
                if (request.isValid())
                {
                    if (endpoint.canProcrss(request))
                    {
                        return endpoint.handleRequest(request);
                    }
                }  
            }
            // wenn nicht gefünden 
            // wenn unsere funktion ist static denn brauchen wir nicht new schreiben
            return ResponseCreator.notFound();
             
        }
    }
}


/*
            switch (request.path)
            {
                case "/users":
                    return 
            }*/
//Endpunkt ep =  endpunktList.FindIndex();
//return endpunktList.ElementAt(0).handleRequest(request);

