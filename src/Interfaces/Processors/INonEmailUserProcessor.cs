using IdentityServer4.Validation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.Processors
{
    public interface INonEmailUserProcessor
    {
        Task<GrantValidationResult> Process(JObject userInfo,string provider);
    }
}
