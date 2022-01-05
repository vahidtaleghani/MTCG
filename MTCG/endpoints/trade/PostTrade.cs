using MTCG.data.repository;
using MTCG.helper;
using MTCG.repository;
using MTCG.repository.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MTCG.endpoints.stats
{
    public class PostTrade : IEndpoint
    {
        private Tradeobject reqTrade;
        struct Tradeobject
        {
            public string Id { get; set; }
            public string CardToTrade { get; set; }
            public string Type { get; set; }
            public double MinimumDamage { get; set; }
        }
        public bool canProcrss(Request request)
        {
            return request.path.Equals("/tradings")
               && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                //kontrolieren Headers
                String user = new Authorize().authorizeUser(request);
                if (user == null)
                    return ResponseCreator.unauthorized("No valid authorization token provided");

                // Kontrolieren Json
                try
                {
                    this.reqTrade = JsonConvert.DeserializeObject<Tradeobject>(request.getPayload());
                    if (String.IsNullOrEmpty(this.reqTrade.Id) || String.IsNullOrEmpty(this.reqTrade.CardToTrade) || String.IsNullOrEmpty(this.reqTrade.Type) || this.reqTrade.MinimumDamage == null)
                        return ResponseCreator.jsonInvalid();
                    if (!new CardReps().ControlCardBelongeToUsername(this.reqTrade.CardToTrade, user))
                        return ResponseCreator.forbidden("This Card does not belong to this user");
                    List<Card> cardslist = new CardReps().getDeckByUsername(user);

                    foreach (var card in cardslist)
                    {
                        if (card.id.Contains(this.reqTrade.CardToTrade))
                            return ResponseCreator.forbidden("This Card is on deck");
                    }
                    if (!new TradeReps().addCardToStore(user, this.reqTrade.Id, this.reqTrade.CardToTrade, this.reqTrade.Type, this.reqTrade.MinimumDamage))
                        return ResponseCreator.jsonInvalid("User could not be updated");
                    Console.WriteLine(user + " added card with id : " + this.reqTrade.Id + " , Card To Trade: " + this.reqTrade.CardToTrade + " Type: " + this.reqTrade.Type + " and MinimumDamage:" + this.reqTrade.MinimumDamage);
                }
                catch (Exception)
                {
                    return ResponseCreator.jsonInvalid();
                }
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
            return ResponseCreator.ok();
        }
    }
}
