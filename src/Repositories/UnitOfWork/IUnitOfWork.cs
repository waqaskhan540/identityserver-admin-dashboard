using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.UnitOfWork
{
    public interface IUnitOfWork<T>
    {
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
