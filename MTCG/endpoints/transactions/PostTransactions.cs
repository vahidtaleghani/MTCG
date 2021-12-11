using MTCG.helper;
using MTCG.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.transactions
{
    public class PostTransactions : IEndpoint
    {
        public bool canProcrss(Request request)
        {
            Response response = new Response();
            return request.path.Equals("/transactions/packages")
                && request.getMethode().Equals(Request.METHODE.POST);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                String username = new Authorize().authorizeUser(request);
                if (username == null)
                    return ResponseCreator.forbidden();

                int coin = new UserReps().getCoinsByUsername(username);

                if(coin <= 0)
                    return ResponseCreator.forbidden("This user does not have enough Coins");

                List<int> packagIdList = new CardReps().getAllFreePackageId();
                if(packagIdList.Count() == 0)
                    return ResponseCreator.forbidden("There is no empty package");
                int randomPackageId = new Random().Next(packagIdList.Count);

                if(new CardReps().updateUsernameOfCards(username, packagIdList[randomPackageId]))
                    if (new UserReps().updateCoinsUser(username, (coin - 5)))
                        return ResponseCreator.ok();
            }
            catch (Exception)
            {
                return ResponseCreator.forbidden();
            }
            return ResponseCreator.forbidden();
        }
    }
}
