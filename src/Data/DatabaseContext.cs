using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class DatabaseContext:IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {


        }

        public DbSet<ExternalUser> ExternalUsers { get; set; }
        public DbSet<PasswordConfigs> PasswordConfigs { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
