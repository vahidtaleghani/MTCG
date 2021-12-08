﻿using MTCG.helper;
using MTCG.repository;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using static MTCG.Response;

namespace MTCG.endpoints.users
{
    public class PutUsers : IEndpoint
    {
        private Userobject reqUsers;
        private String pattern = "/users/";
        struct Userobject
        {
            public string Name { get; set; }
            public string Bio { get; set; }
            public string Image { get; set; }
        }


        public bool canProcrss(Request request)
        {
            Response response = new Response();
            return Regex.IsMatch(request.path, "/users/([0-9a-zA-Z.-]+)")
                && (request.getContentType()==null) ? false : request.getContentType().Equals(response.Content_Type_value[Content_Type.JSON]) == true
                && request.getMethode().Equals(Request.METHODE.PUT);
        }

        public Response handleRequest(Request request)
        {
            try
            {
                //kontrolieren Headers
                String user = new Authorize().authorizeUser(request);
                if (user == null)
                    return ResponseCreator.forbidden();

                String[] substrings = Regex.Split(request.path, this.pattern);
                if (substrings[0] == null || !user.Equals(substrings[1]))
                    return ResponseCreator.notFound();

                // Kontrolieren Json
                try
                {
                    this.reqUsers = JsonConvert.DeserializeObject<Userobject>(request.getPayload());
                    if (String.IsNullOrEmpty(this.reqUsers.Name) || String.IsNullOrEmpty(this.reqUsers.Bio) || String.IsNullOrEmpty(this.reqUsers.Image))
                        return ResponseCreator.jsonInvalid();
                    if (!new UserReps().updateUser(user,this.reqUsers.Name, this.reqUsers.Bio,this.reqUsers.Image))
                        return ResponseCreator.jsonInvalid("User could not be updated");
                    Console.WriteLine("  User with name : " + this.reqUsers.Name + " , Bio: " + this.reqUsers.Bio + " and Image: " + this.reqUsers.Image +" updated");
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
