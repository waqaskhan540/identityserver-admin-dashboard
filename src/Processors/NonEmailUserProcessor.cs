using IdentityServer.Interfaces.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using IdentityServer4.Models;
using IdentityServer.Data;
using IdentityServer.Repositories.UnitOfWork;

namespace IdentityServer.Processors
{
    public class NonEmailUserProcessor : INonEmailUserProcessor
    {
        private readonly IExternalUserRepository _externalUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<DatabaseContext> _unitOfWork;
        public NonEmailUserProcessor(
            IExternalUserRepository externalUserRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork<DatabaseContext> unitOfWork
            )
        {
            _externalUserRepository = externalUserRepository ?? throw new ArgumentNullException(nameof(externalUserRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<GrantValidationResult> Process(JObject userInfo,string provider)
        {

            var userEmail = userInfo.Value<string>("email");

            if (provider.ToLower() == "linkedin")
                userEmail = userInfo.Value<string>("emailAddress");

            var userExternalId = userInfo.Value<string>("id");

            if (userEmail == null)
            {
                var registeredUser =  _externalUserRepository.Get(x => x.ExternalId == userExternalId).FirstOrDefault();
                if (registeredUser == null)
                {
                    var customResponse = new Dictionary<string, object>();
                    customResponse.Add("userInfo", userInfo);                    
                    return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "could not retrieve user's email from the given provider, include email paramater and send request again.", customResponse);                   
                }
                else
                {
                    var existingUser =  _userManager.FindByIdAsync(registeredUser.UserId).Result;
                    var userClaims = _userManager.GetClaimsAsync(existingUser).Result;
                    return new GrantValidationResult(existingUser.Id, provider, userClaims, provider, null);
                }

            }
            else
            {
                var new_user = new ApplicationUser { Email = userEmail,UserName = userEmail };
                var result =  _userManager.CreateAsync(new_user).Result;
                if (result.Succeeded)
                {
                    await _externalUserRepository.AddAsync(new ExternalUser { ExternalId = userExternalId, Provider = provider, UserId = new_user.Id });
                    await _unitOfWork.SaveChangesAsync();
                    var userClaims = await _userManager.GetClaimsAsync(new_user);
                    return new GrantValidationResult(new_user.Id, provider, userClaims, provider, null);
                }
                return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "user could not be created, please try again");
            }

        }
    }
}
