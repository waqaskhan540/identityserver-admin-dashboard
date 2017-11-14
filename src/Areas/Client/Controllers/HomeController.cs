using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
