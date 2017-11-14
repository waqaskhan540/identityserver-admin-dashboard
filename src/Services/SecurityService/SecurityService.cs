using IdentityServer.Data.Entities;
using IdentityServer.Interfaces.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Extensions;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Constants;
using IdentityServer.Repositories.Interfaces;

namespace IdentityServer.Services.SecurityService
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IPasswordConfigRepository _passwordConfigRepository;
        public SecurityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IPasswordConfigRepository passwordConfigRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _passwordConfigRepository = passwordConfigRepository;
        }

        public async Task<BaseModel> Login(string clientID, string clientSecret, string email, string password, string scopes)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);

                if (!user.ClientId.ToLower().Equals(clientID))
                    return BaseModel.Error(Messages.USER_INVALID);

                if (!user.EmailConfirmed)
                    return BaseModel.Error(Messages.EMAIL_NOT_CONFIRMED);

                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!loginResult.Succeeded)
                    return BaseModel.Error(Messages.INVALID_USERNAME_PASSWORD);

                var tokenData = await _tokenService.GetToken(clientID, clientSecret, email, password, scopes);
                if (tokenData == null)
                    return BaseModel.Error(Messages.GENERATE_TOKEN_FAILED);

                return tokenData;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<BaseModel> CreateUser<TMap>(ApplicationUser userModel, string password,bool createActivated) where TMap:class,IIdentityResult,new()
        {
            try
            {
                var user = _userManager.FindByClientId(userModel.ClientId, userModel.Email);
                if (user != null)
                    return BaseModel.Error(Messages.USER_ALREADY_EXISTS);

                userModel.EmailConfirmed = createActivated;

                var passwordConfig = _passwordConfigRepository.Get()
                                            .FirstOrDefault(x => x.ClientId.ToLower() == userModel.ClientId.ToLower());

                if(passwordConfig != null)
                {
                    _userManager.Options.Password.RequireDigit = passwordConfig.RequireDigit;
                    _userManager.Options.Password.RequiredLength = passwordConfig.RequiredLength;
                    _userManager.Options.Password.RequiredUniqueChars = passwordConfig.RequiredUniqueChars;
                    _userManager.Options.Password.RequireLowercase = passwordConfig.RequireLowercase;
                    _userManager.Options.Password.RequireNonAlphanumeric = passwordConfig.RequireNonAlphanumeric;
                    _userManager.Options.Password.RequireUppercase = passwordConfig.RequireUppercase;
                }

                var result = await _userManager.CreateAsync(userModel, password);
                
                if (result.Succeeded)
                {
                    var emailConfirmationToken = createActivated ? await _userManager.GenerateEmailConfirmationTokenAsync(user) : null;
                    await _userManager.AddClaimAsync(userModel, new Claim(JwtClaimTypes.Email, user.Email));

                    var map = new TMap();
                    var data = map.Transform<ApplicationUser, string>(userModel, emailConfirmationToken);

                    return BaseModel.Success(Messages.USER_REGISTER_SUCCESS, data);
                }

                var message = result.Errors.Aggregate("", (current, error) => current + (error.Description + "\n")).TrimEnd('\n');
                return BaseModel.Error(message);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<BaseModel> AddUserClaim(string userId, string claimType, string claimValue)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BaseModel.Error(Messages.USER_NOT_FOUND);
                }

                var userClaims = await _userManager.GetClaimsAsync(user);

                if (userClaims.Any())
                {
                    var givenClaim = userClaims.FirstOrDefault(x => x.Type.ToLower() == claimType.ToLower() && x.Value.ToLower() == claimValue.ToLower());

                    if (givenClaim != null)
                    {
                        return BaseModel.Success(message: Messages.CLAIM_ALREADY_EXIST);
                    }
                }
                var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));

                if (result.Succeeded)
                {
                    return BaseModel.Success(message: Messages.CLAIM_ADDED);
                }

                var message = result.Errors.FirstOrDefault() != null
                    ? result.Errors.FirstOrDefault().Description
                    : Messages.CLAIM_ADDING_FAILED;

                return BaseModel.Error(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BaseModel> RemoveUserClaim(string userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BaseModel.Error(Messages.USER_NOT_FOUND);
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            if (userClaims.Any())
            {
                var givenClaim = userClaims.FirstOrDefault(x => x.Type.ToLower() == claimType.ToLower() && x.Value.ToLower() == claimValue.ToLower());

                if (givenClaim == null)
                {
                    return BaseModel.Success(message: Messages.USER_CLAIM_NOT_ASSIGNED);

                }
            }

            var result = await _userManager.RemoveClaimAsync(user, new Claim(claimType, claimValue));

            if (result.Succeeded)
            {
                return BaseModel.Success(message: Messages.CLAIM_REMOVE_SUCCESS);

            }

            return BaseModel.Error(result.Errors.FirstOrDefault() != null ?
                result.Errors.FirstOrDefault().Description : Messages.CLAIM_REMOVE_FAILED);
        }

       
        public async Task<BaseModel> ChangePassword(string email, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (result.Succeeded)
                    return BaseModel.Success(Messages.PASSWORD_CHANGE_SUCCESS);

                var message = result.Errors.FirstOrDefault() != null ? result.Errors.FirstOrDefault().Description : Messages.PASSWORD_CHANGE_FAILED;
                return BaseModel.Error(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public async Task<BaseModel> GetEmailVerificationToken(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);
                }

                var varficationCode = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                if (string.IsNullOrWhiteSpace(varficationCode))
                {
                    return BaseModel.Error(Messages.GENERATE_EMAIL_CODE_FAILED);
                }

                return BaseModel.Success(varficationCode, message: Messages.GENERATE_EMAIL_CODE_SUCCESS);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BaseModel> GetPasswordResetToken(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);
                }

                var resetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
                return BaseModel.Success(new { userId = user.Id, resetCode = resetCode });
            }
            catch (Exception)
            {

                throw;
            }
        }



 

        public async Task<BaseModel> ResetPassword(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BaseModel.Error(Messages.USER_NOT_EXISTS);
            }


            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (result.Succeeded)
            {
                return BaseModel.Success(message:Messages.PASSWORD_CHANGE_SUCCESS);
            }

            return BaseModel.Create(true,
                result.Errors.FirstOrDefault() != null
                    ? result.Errors.FirstOrDefault().Description
                    : Messages.PASSWORD_RESET_FAILED);
        }

        public async Task<BaseModel> UserExists(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);

                return BaseModel.Success(Messages.USER_EXISTS);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BaseModel> VerifyEmail(string userId, string verificationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return BaseModel.Error(Messages.USER_NOT_EXISTS);
                }

                var response = await _userManager.ConfirmEmailAsync(user, verificationToken);

                if (response.Succeeded)
                {
                    return BaseModel.Success(message: Messages.EMAIL_CONFIRM_SUCCESS);
                }
                var message = response.Errors.FirstOrDefault() != null
                    ? response.Errors.FirstOrDefault().Description
                    : Messages.EMAIL_CONFIRM_FAILED;

                return
                    BaseModel.Error(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
