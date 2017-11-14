using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);
        
    }
}
