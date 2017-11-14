using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string ClientId { get; set; }
    }
}
