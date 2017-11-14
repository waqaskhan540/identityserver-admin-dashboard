using IdentityServer.Interfaces.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services.SecurityService.Results
{
    public class RegisterationResult : IIdentityResult
    {
        public string emailConfirmationToken { get; set; }
        public string userId { get; set; }
        public string email { get; set; }
        public object Transform<T, TKey>(T user, object token = null)
            where T : IdentityUser
            where TKey : IEquatable<TKey>
        {
            emailConfirmationToken = token != null ? token.ToString() : "";
            userId = user.Id?.ToString();
            email = user.Email;
            return this;
        }
    }
}
