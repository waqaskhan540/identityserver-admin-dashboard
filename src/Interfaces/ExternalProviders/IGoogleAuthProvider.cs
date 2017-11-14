﻿using IdentityServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.ExternalProviders
{
    public interface IGoogleAuthProvider:IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
