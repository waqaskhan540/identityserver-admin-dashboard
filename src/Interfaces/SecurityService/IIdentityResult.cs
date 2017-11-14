using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.SecurityService
{
    public interface IIdentityResult
    {
        Object Transform<T, TKey>(T user, object extra = null)
            where TKey : IEquatable<TKey>
            where T : IdentityUser;

    }
}
