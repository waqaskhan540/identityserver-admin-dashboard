﻿using IdentityServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data.Entities;
using Newtonsoft.Json.Linq;
using IdentityServer.Repositories.Interfaces;
using System.Net.Http;
using IdentityServer.Helpers;
using IdentityServer.Interfaces.ExternalProviders;

namespace IdentityServer.Providers
{
    public class LinkedInAuthProvider : ILinkedInAuthProvider
    {
        private readonly IExternalUserRepository _externalUserRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;
        public LinkedInAuthProvider(
             IExternalUserRepository externalUserRepository,
             IProviderRepository providerRepository,
             HttpClient httpClient
             )
        {
            _externalUserRepository = externalUserRepository;
            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }
        public Provider Provider => _providerRepository.Get()
                                    .FirstOrDefault(x => x.Name.ToLower() == ProviderType.LinkedIn.ToString().ToLower());

        public JObject GetUserInfo(string accessToken)
        {
            var urlParams = $"oauth2_access_token={accessToken}&format=json";

            var result = _httpClient.GetAsync($"{Provider.UserInfoEndPoint}{urlParams}").Result;
            if (result.IsSuccessStatusCode)
            {
                var infoObject = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                return infoObject;
            }
            return null;
        }
    }
}
