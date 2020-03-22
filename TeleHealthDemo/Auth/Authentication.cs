using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleHealthDemo.Auth.Interfaces;
using Microsoft.AspNetCore.Http;

namespace TeleHealthDemo.Auth
{
    public class Authentication : IAuthentication
    {   
        public bool IsAuthenticated(HttpRequest request)
        {
            var authKeyProvided = request.Headers.ContainsKey("Authorization");
            var authKey = authKeyProvided ? request.Headers.ToList().FirstOrDefault(k => k.Key == "Authorization").Value[0] : string.Empty;

            // instead of a hardcoded value, this key should be coming from a secret file which is bound at application startup. 
            // and those keys can be injected at deployment time from a secure location like Azure Key Vault instead of keeping them within the solution
            if (string.IsNullOrEmpty(authKey) || authKey != "TXlBdXRob3JpemF0aW9uVG9rZW4=")
            {
                return false;
            }

            return true;
        }
    }
}
