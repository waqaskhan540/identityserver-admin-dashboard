using IdentityServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data.Entities;
using IdentityServer.Helpers;
using IdentityServer.Data;

namespace IdentityServer.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
