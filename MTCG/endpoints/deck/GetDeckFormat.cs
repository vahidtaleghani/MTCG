using MTCG.helper;
using MTCG.repository;
using MTCG.repository.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTCG.endpoints.deck
{
    public class GetDeckFormat : IEndpoint

    {
        private String pattern = "/deck[?]format=";
        public bool canProcrss(Request request)
        {
            return Regex.IsMatch(request.path, "/deck[?]format=([0-9a-zA-Z.-]+)")
                 && request.getMethode().Equals(Request.METHODE.GET);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String[] substrings = Regex.Split(request.path, this.pattern);
                if (!substrings[1].ToUpper().Equals("PLAIN"))
                    return ResponseCreator.forbidden("There is no Plain format");
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden("There is no token");

                List<Card> cardList = new CardReps().getDeckByUsername(username);
                if (cardList.Count == 0)
                    return ResponseCreator.ok("The deck is not configured");
                
                string plainResponse = null;
                foreach (var card in cardList)
                {
                    plainResponse += ($"{card.id},{card.name},{card.damage}\r\n");
                }
                return ResponseCreator.okPlainPayload(plainResponse);
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden("error");
            }
        }
    }
}
