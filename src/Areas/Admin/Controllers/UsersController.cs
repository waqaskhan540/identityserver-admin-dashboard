using IdentityServer.Data.Entities;
using IdentityServer.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.GetAllUsers();
            return View(users);
        }
    }
}
