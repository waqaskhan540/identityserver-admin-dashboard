using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.Interfaces.IdentityServerRepositories
{
    public interface IResourcesRepository : IResourceStore
    {
        IQueryable<ApiResource> GetApiResources();
        IQueryable<IdentityResource> GetIdentityResources();
    }
}
