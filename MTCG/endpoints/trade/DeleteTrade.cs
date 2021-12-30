using MTCG.data.entity;
using MTCG.data.repository;
using MTCG.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTCG.endpoints.trade
{
    public class DeleteTrade : IEndpoint
    {
        private String pattern = "/tradings/";
        public bool canProcrss(Request request)
        {
            return Regex.IsMatch(request.path, "/tradings/([0-9a-zA-Z.-]+)")
                && request.getMethode().Equals(Request.METHODE.DELETE);
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
                string id = substrings[1];
                Trade trade = new TradeReps().getTradeById(id);
                if (trade == null)
                    return ResponseCreator.forbidden("This Id does not exist for trade");
                if (!user.Equals(trade.username))
                    return ResponseCreator.forbidden("This Id doese not belongs to this user");
                if (!new TradeReps().deleteCardInStoredById(id))
                    return ResponseCreator.serverError();
                return ResponseCreator.ok();
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
        }
    }
}
