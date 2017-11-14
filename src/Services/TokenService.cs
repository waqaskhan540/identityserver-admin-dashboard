using IdentityServer.Interfaces.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
       
        public TokenService()
        {
            
        }
        public async Task<BaseModel> GetToken(string clientId, string clientSecret, string email, string password, string scopes)
        {
            try
            {
                var authority = "[authority]";
                var tokenClient = new TokenClient(authority + "/connect/token", clientId, clientSecret);

                var response = await tokenClient.RequestResourceOwnerPasswordAsync(email, password, scopes);

                if (!response.IsError && response.Json != null)
                    return BaseModel.Success("", response.Json);

                if (response.IsError)
                {
                    var error = response.Json["error"].ToString();

                    JToken errorDescription;
                    var hasDescription = response.Json.TryGetValue("error_description", out errorDescription);

                    if (hasDescription)
                        return BaseModel.Error(errorDescription.ToString());

                    return BaseModel.Error(error);
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
