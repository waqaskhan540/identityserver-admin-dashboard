using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Client.Controllers
{
    [Area("Client")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
