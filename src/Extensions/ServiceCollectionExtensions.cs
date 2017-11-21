using IdentityServer4.Validation;
using IdentityServer.Configuration;
using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.ExtensionGrant;
using IdentityServer.Interfaces;
using IdentityServer.Interfaces.Processors;
using IdentityServer.Processors;
using IdentityServer.Providers;
using IdentityServer.Repositories;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using IdentityServer.Interfaces.SecurityService;
using IdentityServer.Interfaces.ExternalProviders;
using IdentityServer.Services;
using IdentityServer.Services.SecurityService;
using IdentityServer.Constants;
using IdentityModel;
using IdentityServer.Repositories.UnitOfWork;
using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer.Repositories.IdentityServerRepositories;
using IdentityServer.Interfaces.IdentityServer;
using IdentityServer.Services.IdentityServer;
using IdentityServer.Dashboard.Configuration.TestConfig;
using IdentityServer4;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<DatabaseContext>(cfg => cfg.UseMySql(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<DatabaseContext>()
                  .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services,string connectionString)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
               .AddConfigurationStore(opt => opt.ConfigureDbContext = builder => {
                   builder.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)); })
               .AddOperationalStore(opt => opt.ConfigureDbContext = builder => {
                   builder.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)); })
               .AddConfigurationStoreCache()
               .AddAspNetIdentity<ApplicationUser>();

           
            return services;
        }

        public static IServiceCollection AddTestIdentityServer(this IServiceCollection services)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(TestConfig.GetClients())
                .AddInMemoryApiResources(TestConfig.GetApiResources())
                .AddInMemoryIdentityResources(TestConfig.GetIdentityResources())
                .AddTestUsers(TestConfig.GetUsers());
            // .AddInMemoryPersistedGrants();

            services.AddAuthentication()
              .AddGoogle("Google", options =>
              {
                  options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                  options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                  options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
              })
              .AddOpenIdConnect("oidc", "OpenID Connect", options =>
              {
                  options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                  options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                  options.Authority = "https://demo.identityserver.io/";
                  options.ClientId = "implicit";

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      NameClaimType = "name",
                      RoleClaimType = "role"
                  };
              });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped<INonEmailUserProcessor, NonEmailUserProcessor>();
            services.AddScoped<IEmailUserProcessor, EmailUserProcessor>();
            services.AddScoped<IExtensionGrantValidator, ExternalAuthenticationGrant>();
            services.AddScoped<IClientService, ClientService>();
            services.AddSingleton<HttpClient>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
          
            services.AddScoped<IExternalUserRepository, ExternalUserRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IPasswordConfigRepository, PasswordConfigRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IResourcesRepository, ResourceRepository>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            return services;
        }

        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IFacebookAuthProvider, FacebookAuthProvider>();
            services.AddTransient<ITwitterAuthProvider, TwitterAuthProvider>();
            services.AddTransient<IGoogleAuthProvider, GoogleAuthProvider>();
            services.AddTransient<ILinkedInAuthProvider, LinkedInAuthProvider>();
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Policies.ID_SERVER_CORS_POLICY, builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            return services;
        }

        public static IServiceCollection AddAdminAccessPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ID_SERVER_ADMIN_POLICY, builder => builder.RequireClaim(JwtClaimTypes.Role, Policies.ID_SERVER_ADMIN_REQUIRED_CLAIM));
            });
            return services;
        }
    }
}
