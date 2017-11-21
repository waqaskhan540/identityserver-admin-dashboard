using IdentityModel;
using IdentityServer.Data.Entities;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.Configuration
{
    public class Config
    {
        // scopes define the resources in your systems

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityServer4.Models.IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Test.WebApi","Test WebApi")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Test.Client",
                    ClientName = "LDSCore",
                    AllowedGrantTypes = new[] {GrantType.ResourceOwnerPassword,"external"},
                     
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "Test.WebApi",
                        StandardScopes.Email,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 86400,
                    IdentityTokenLifetime = 86400,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(JwtClaimTypes.Name, "jwtName")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }

        public static IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>
            {
                new Provider
                {                  
                    Name = "Facebook",
                    UserInfoEndPoint = "https://graph.facebook.com/v2.8/me"
                },
                new Provider
                {                    
                    Name = "Google",
                    UserInfoEndPoint = "https://www.googleapis.com/oauth2/v2/userinfo"
                },
                 new Provider
                {                    
                    Name = "Twitter",
                    UserInfoEndPoint = "https://api.twitter.com/1.1/account/verify_credentials.json"
                },
                 new Provider
                 {                     
                     Name="LinkedIn",
                     UserInfoEndPoint = "https://api.linkedin.com/v1/people/~:(id,email-address,first-name,last-name,location,industry,picture-url)?"
                 }
            };
        }
        public static List<PasswordConfigs> GetPasswordConfigs()
        {
            return new List<PasswordConfigs>
            {
                new PasswordConfigs{ Id = 1, ClientId = "Test.Client"},

            };
        }
    }
}
