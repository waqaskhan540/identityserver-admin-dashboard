using IdentityServer.Constants;
using IdentityServer.Data.Entities;
using IdentityServer.Interfaces.SecurityService;
using IdentityServer.Models;
using IdentityServer.Services.SecurityService;
using IdentityServer.Services.SecurityService.Results;
using IdentityServer.ViewModels.SecurityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers.Api
{
    [Route("api/account")]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ValidateModelState]
        public async Task<BaseModel> Register(RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    ClientId = model.ClientID
                };
                return await _securityService.CreateUser<RegisterationResult>(user, model.Password, model.CreateActivated);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateModelState]
        public async Task<BaseModel> Login(LoginViewModel model)
        {
            try
            {
                return await _securityService.Login(model.ClientId, model.ClientSecret, model.Email, model.Password, model.Scopes);
            }
            catch (Exception ex)
            {

                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("GeneratePasswordResetToken")]
        [AllowAnonymous]
        public async Task<BaseModel> GetPasswordResetToken(string email)
        {
            try
            {
                return await _securityService.GetPasswordResetToken(email);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<BaseModel> ResetPassword(string userId,string token,string newPassword)
        {
            try
            {
                return await _securityService.ResetPassword(userId, token, newPassword);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

      [HttpPost("AddUserClaim")]
      //[Authorize(Policy = Policies.ID_SERVER_ADMIN_POLICY)]
      public async Task<BaseModel> AddUserClaim(string userId,string claimType,string claimValue)
        {
            try
            {
                return await _securityService.AddUserClaim(userId, claimType, claimValue);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("RemoveUserClaim")]
        //[Authorize(Policy = Policies.ID_SERVER_ADMIN_POLICY)]
        public async Task<BaseModel> RemoveUserClaim(string userId,string claimType,string claimValue)
        {
            try
            {
                return await _securityService.RemoveUserClaim(userId, claimType, claimValue);                
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("UserExists")]
        public async Task<BaseModel> UserExists(string email)
        {
            try
            {
                return await _securityService.UserExists(email);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<BaseModel> ChangePassword(string email,string currentPassword,string newPassword)
        {
            try
            {
                return await  _securityService.ChangePassword(email, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("GenerateEmailVerificationToken")]
        public async Task<BaseModel> GetEmailVerificationToken(string email)
        {
            try
            {
                return await _securityService.GetEmailVerificationToken(email);
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        [HttpPost("VerifyEmail")]
        public async Task<BaseModel> VerifyEmail(string email,string verificationCode)
        {
            try
            {
                return await _securityService.VerifyEmail(email, verificationCode);
                
            }
            catch (Exception ex)
            {
                return BaseModel.Error(ex.Message);
            }
        }

        
    }
}
