using IdentityServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using IdentityServer.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public  IQueryable<T> Get()
        {
            return _dbSet.AsQueryable() ;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
