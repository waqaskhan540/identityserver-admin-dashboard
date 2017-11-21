using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer.Repositories.IdentityServerRepositories
{
    public class ResourceRepository : ResourceStore, IResourcesRepository
    {
        private readonly IConfigurationDbContext _dbContext;
        public ResourceRepository(IConfigurationDbContext context, ILogger<ResourceStore> logger) : base(context, logger)
        {
            _dbContext = context;
        }

        public IQueryable<ApiResource> GetApiResources()
        {
            return _dbContext.ApiResources.AsQueryable();
        }

        public IQueryable<IdentityResource> GetIdentityResources()
        {
            return _dbContext.IdentityResources.AsQueryable();
        }
    }
}
