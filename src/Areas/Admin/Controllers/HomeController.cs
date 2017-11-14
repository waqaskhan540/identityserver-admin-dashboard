using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers
{
    public class HomeController:Controller
    {
        [Area("Admin")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
