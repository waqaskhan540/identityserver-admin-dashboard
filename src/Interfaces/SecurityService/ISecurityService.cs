using IdentityServer.Data.Entities;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.SecurityService
{
    public interface ISecurityServiceBase<T> where T: IdentityUser
    {
        Task<BaseModel> CreateUser<TMap>(T user, string password,bool createActivated) where TMap : class, IIdentityResult, new();
        Task<BaseModel> Login(string clientID, string clientSecret, string email, string password, string scopes);
        Task<BaseModel> GetPasswordResetToken(string email);
        Task<BaseModel> GetEmailVerificationToken(string email);
        Task<BaseModel> VerifyEmail(string userId, string verificationToken);
        Task<BaseModel> ResetPassword(string userId, string resetToken, string newPassword);

        Task<BaseModel> AddUserClaim(string userId, string claimType, string claimValue);
        Task<BaseModel> RemoveUserClaim(string userId, string claimType, string claimValue);

        Task<BaseModel> UserExists(string email);
        Task<BaseModel> ChangePassword(string email, string currentPassword, string newPassword);

        

    }

    public interface ISecurityService: ISecurityServiceBase<ApplicationUser>
    {

    }
}
