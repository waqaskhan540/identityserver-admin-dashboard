using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data.Entities
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string UserInfoEndPoint { get; set; }
    }
}
