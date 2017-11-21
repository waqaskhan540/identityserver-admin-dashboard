using IdentityServer4.EntityFramework.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repositories.IdentityServerRepositories
{
    public class ClientRepository : ClientStore,IClientRepository
    {
        private readonly IConfigurationDbContext _dbContext;
        private readonly DbSet<Client> _dbSet;
        public ClientRepository(IConfigurationDbContext context, ILogger<ClientStore> logger) : base(context, logger)
        {
            _dbContext = context;
            _dbSet = _dbContext.Clients;

        }

        public async Task AddAsync(Client entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<Client> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Client entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(IEnumerable<Client> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public IQueryable<Client> Get()
        {
            return _dbContext.Clients.AsQueryable();
        }

        public IQueryable<Client> Get(Expression<Func<Client, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Update(Client entity)
        {
           // _dbSet.Attach(entity);
             _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Update(IEnumerable<Client> entities)
        {
            throw new NotImplementedException();
        }
    }
}
