using MTCG.endpoints;
using MTCG.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTCG.Response;

namespace MTCG
{
    public class EndpointController
    {
        List<IEndpoint> endpointList = new List<IEndpoint>();

        public EndpointController()
        {
            endpointList.Add(new HelloWord());
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

