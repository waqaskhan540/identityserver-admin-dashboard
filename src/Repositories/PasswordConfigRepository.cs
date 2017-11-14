using IdentityServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data.Entities;
using IdentityServer.Configuration;
using IdentityServer.Data;

namespace IdentityServer.Repositories
{
    public class PasswordConfigRepository : GenericRepository<PasswordConfigs>, IPasswordConfigRepository
    {
        public PasswordConfigRepository(DatabaseContext dbContext) : base(dbContext)
        {
            

        }
    }
}
