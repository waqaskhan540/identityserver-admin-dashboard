using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data.Entities
{
    public class PasswordConfigs
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int RequiredLength { get; set; } = 6;
        public int RequiredUniqueChars { get; set; } = 1;
        public bool RequireNonAlphanumeric { get; set; } = false;
        public bool RequireLowercase { get; set; } = false;
        public bool RequireUppercase { get; set; } = false;
        public bool RequireDigit { get; set; } = false;
    }
}
