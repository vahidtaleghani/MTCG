using MTCG.endpoints;
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
            Response response = new Response();
            response.Send(Response.StatusCode.Not_Found, response.status_code_value[StatusCode.Not_Found]);
            return response;
             
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

