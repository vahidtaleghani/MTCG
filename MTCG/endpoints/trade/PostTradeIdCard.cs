using MTCG.data.entity;
using MTCG.data.repository;
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

namespace MTCG.endpoints.trade
{
    public class PostTradeIdCard : IEndpoint
    {
        private String pattern = "/tradings/";
        public bool canProcrss(Request request)
        {
            return Regex.IsMatch(request.path, "/tradings/([0-9a-zA-Z.-]+)")
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                //kontrolieren Headers
                String user = new Authorize().authorizeUser(request);
                if (user == null)
                    return ResponseCreator.forbidden();
                //split to take id
                String[] substrings = Regex.Split(request.path, this.pattern);

                Trade trade = new TradeReps().getTradeById(substrings[1]);
                if (trade == null)
                    return ResponseCreator.forbidden("This Id does not exist for trade");
                if(user.Equals(trade.username))
                    return ResponseCreator.forbidden("This Id belongs to this user");

                Console.WriteLine(request.getPayload());
                String payloadId = JsonConvert.DeserializeObject<String>(request.getPayload());

                Console.WriteLine(payloadId);
                
                Card card = new CardReps().getCardById(payloadId);
                if (card == null)
                    return ResponseCreator.forbidden("This Card does not exist");
                
                if(card.username.Equals(trade.username))
                    return ResponseCreator.forbidden("This Card belongs to this user");
                
                if(card.damage < trade.min_damage)
                    return ResponseCreator.forbidden("The damage to this card is less than the damage requested");

                if(trade.card_type.ToLower().Equals("spell") && card.card_type.ToLower().Equals("spell") || !trade.card_type.ToLower().Equals("spell") && !card.card_type.ToLower().Equals("spell"))
                {
                    if(new TradeReps().updateStoredById(substrings[1]) && 
                        new CardReps().updateCardByUsername(trade.card_trade_id, user) &&
                        new CardReps().updateCardByUsername(payloadId, trade.username))
                        return ResponseCreator.ok();
                }
               
                return ResponseCreator.forbidden("The type of this card does not match the type of card requested");
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }    
        }
    }
}
