using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.Interfaces.IdentityServerRepositories
{
    public interface IClientRepository : IClientStore,IRepository<Client>
    {
        
    }
}
