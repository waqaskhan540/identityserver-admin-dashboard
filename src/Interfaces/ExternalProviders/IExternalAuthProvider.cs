using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.ExternalProviders
{
    public interface IExternalAuthProvider
    {
        JObject GetUserInfo(string accessToken);
    }
}
