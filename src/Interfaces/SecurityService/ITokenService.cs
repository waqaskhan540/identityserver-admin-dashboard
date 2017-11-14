using IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.SecurityService
{
    public interface ITokenService
    {
        Task<BaseModel> GetToken(string clientId, string clientSecret, string email, string password, string scopes);
    }
}
