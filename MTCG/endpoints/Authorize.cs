using MTCG.repository;
using System;

namespace MTCG.endpoints
{
    public class Authorize
    {
        public String authorizeUser(Request request)
        {
            if (!request.getHeaders().ContainsKey("Authorization"))
                return null;

            String token = (String)request.getHeaders()["Authorization"];

            if (token.Length < 7 || !token.StartsWith("Basic "))
                return null;

            token = token.Substring(6);

            return new UserReps().getUsernameByToken(token);
        }
    }
}


/*
  if (!request.getHeaders().ContainsKey("Authorization"))
                return ResponseCreator.forbidden("Header is not Contain of Authorization");

            String token = (String)request.getHeaders()["Authorization"];

            if (token.Length < 7 || !token.StartsWith("Basic "))
                return ResponseCreator.badRequest("Authorization field is not transmitted correctly");
 */