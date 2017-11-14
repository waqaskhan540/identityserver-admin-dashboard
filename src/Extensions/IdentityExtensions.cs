using IdentityModel;
using IdentityServer.Configuration;
using IdentityServer.Constants;
using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Extensions
{
    public static class IdentityExtensions
    {
        public static void EnsureDatabaseSeeded(this IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDb = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                var configurationDb = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService <UserManager<ApplicationUser>>();

                if (configurationDb.AllMigrationsApplied())
                {

                    if (!configurationDb.Clients.Any())
                    {
                        foreach (var client in Config.GetClients())
                            configurationDb.Clients.Add(client.ToEntity());
                    }

                    if (!configurationDb.ApiResources.Any())
                    {
                        foreach (var apiResource in Config.GetApiResources())
                            configurationDb.ApiResources.Add(apiResource.ToEntity());
                    }

                    if (!configurationDb.IdentityResources.Any())
                    {
                        foreach (var identityResource in Config.GetIdentityResources())
                            configurationDb.IdentityResources.Add(identityResource.ToEntity());
                    }

                    configurationDb.SaveChanges();
                }

                if (applicationDb.AllMigrationsApplied())
                {
                    if (!applicationDb.Providers.Any())
                    {
                        foreach (var provider in Config.GetProviders())
                            applicationDb.Providers.Add(provider);
                    }

                    if (!applicationDb.Users.Any())
                    {
                        var superAdmin = new ApplicationUser
                        {
                            Email = "identity.admin@abc.com",
                            UserName = "identity.admin@abc.pk",
                        };

                        var result = userManager.CreateAsync(superAdmin, "identity@123").Result;
                        if(result.Succeeded)
                        {
                            var adminUser = userManager.FindByEmailAsync(superAdmin.Email).Result;

                            //I don't know why I need to do this

                            if(adminUser != null)
                            {
                               var roleClaimResult = userManager.AddClaimAsync(adminUser, new Claim(JwtClaimTypes.Role, Roles.ROLE_SUPER_ADMIN)).Result;
                                if (!roleClaimResult.Succeeded)                                
                                    throw new Exception("could not assign role claim");

                                var subClaimResult = userManager.AddClaimAsync(adminUser, new Claim(JwtClaimTypes.Subject, adminUser.Id)).Result;
                                if (!subClaimResult.Succeeded)
                                    throw new Exception("could not assign subject claim");

                                var nameClaimResult = userManager.AddClaimAsync(adminUser, new Claim(JwtClaimTypes.Name, adminUser.Email)).Result;
                                if (!nameClaimResult.Succeeded)
                                    throw new Exception("could not assign name claim");
                            }
                        }
                    }
                    applicationDb.SaveChanges();
                }

            }
        }
    }
}
