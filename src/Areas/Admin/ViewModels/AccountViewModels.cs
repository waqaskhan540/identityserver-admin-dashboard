using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
