using IdentityServer.Constants;
using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByClientId(this UserManager<ApplicationUser> userManager,string clientId)
        {
            return userManager.Users.FirstOrDefault(x => x.ClientId.ToLower() == clientId.ToLower());
        }

        public static ApplicationUser FindByClientId(this UserManager<ApplicationUser> userManager, string clientId,string email)
        {
            return userManager
                .Users
               .FirstOrDefault(x => x.ClientId.ToLower().Equals(clientId.ToLower()) && x.Email.Equals(email));
        }

        public static async Task<bool> IsSuperAdmin(this UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            if (!claims.Any())
                return false;

            return claims.
                    FirstOrDefault(x => x.Type == ClaimTypes.Role && x.Value == Roles.ROLE_SUPER_ADMIN) == null;
           
        }

        public static  List<ApplicationUser> GetAllUsers(this UserManager<ApplicationUser> userManager)
        {
            return userManager.Users.ToList();
        }
    }
}
