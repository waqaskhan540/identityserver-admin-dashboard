using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Extensions
{

    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                          .GetAppliedMigrations()
                          .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                        .Migrations
                        .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void RunAllPendingMigrations(this DbContext context)
        {
            if (!context.AllMigrationsApplied())
                context.Database.Migrate();
        }
    }
}
