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
    public class PostٍExchangeCard : IEndpoint
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
                String user = new Authorize().authorizeUser(request);
                if (user == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");

                String[] substrings = Regex.Split(request.path, this.pattern);


                String trade_id = substrings[1];
                String card_buyer_id = JsonConvert.DeserializeObject<String>(request.getPayload());

                Trade trade = new TradeReps().getTradeById(trade_id);
                Card card = new CardReps().getCardById(card_buyer_id);

                //check id in trade table exists
                if (trade == null)
                    return ResponseCreator.forbidden("This Id does not exist for trade");
                //chack id of card in cards table exists
                if (card == null)
                    return ResponseCreator.forbidden("This Card does not exist");
                //check id belongs to this user
                if (!new CardReps().ControlCardBelongeToUsername(card_buyer_id,user))
                    return ResponseCreator.forbidden("This Card does not belong to this user");
                //buyer and seller doese not same
                if (trade.username.Equals(card.username))
                    return ResponseCreator.forbidden("Buyer and Seller are same!");
                //check minimum damage to provide
                if (trade.min_damage > card.damage)
                    return ResponseCreator.forbidden("The damage of this card is less than the damage requested");
                //check type of card to match
                if (trade.card_type.ToLower().Equals("spell") && card.card_type.ToLower().Equals("spell") ||
                   !trade.card_type.ToLower().Equals("spell") && !card.card_type.ToLower().Equals("spell"))
                {
                    if (new TradeReps().updateStoredById(substrings[1]) &&
                        new CardReps().updateCardByUsername(trade.card_trade_id, user) &&
                        new CardReps().updateCardByUsername(card_buyer_id, trade.username))
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
