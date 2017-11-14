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
    public class EmailUserProcessor : IEmailUserProcessor
    {
        private readonly IExternalUserRepository _externalUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<DatabaseContext> _unitOfWork;
        public EmailUserProcessor(
            IExternalUserRepository externalUserRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork<DatabaseContext> unitOfWork
            )
        {
            _externalUserRepository = externalUserRepository ?? throw new ArgumentNullException(nameof(externalUserRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<GrantValidationResult> Process(JObject userInfo,string email,string provider)
        {
            var userEmail = email;
            var userExternalId = userInfo.Value<string>("id");

            if (string.IsNullOrWhiteSpace(userExternalId))
            {
                return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "could not retrieve user Id from the token provided");
               
            }

            var existingUser = _userManager.FindByEmailAsync(userEmail).Result;
            if(existingUser != null)
            {
                return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "User with specified email already exists");               
            }

            var new_user = new ApplicationUser { Email = userEmail ,UserName = userEmail};
            var identityResult = await _userManager.CreateAsync(new_user);
            if (identityResult.Succeeded)
            {
                await _externalUserRepository.AddAsync(new ExternalUser { ExternalId = userExternalId, Provider = provider, UserId = new_user.Id }).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync();
                var userClaims = _userManager.GetClaimsAsync(new_user).Result;
                return new GrantValidationResult(new_user.Id, provider, userClaims, provider, null);
              
            }
            return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "could not create user , please try again.");
           
        }
    }
}
