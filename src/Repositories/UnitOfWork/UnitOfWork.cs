using IdentityServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T: DbContext
    {
        private readonly T _dbContext;
        public UnitOfWork(T dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
