using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories
{


    public class ExternalUserRepository : GenericRepository<ExternalUser>, IExternalUserRepository
    {
        public ExternalUserRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
