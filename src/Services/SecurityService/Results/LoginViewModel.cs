using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services.SecurityService.Results
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Scopes { get; set; }
    }
}
