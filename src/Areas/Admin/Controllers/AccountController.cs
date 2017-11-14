using IdentityServer.Areas.Admin.ViewModels.AccountViewModels;
using IdentityServer.Constants;
using IdentityServer.Data.Entities;
using IdentityServer.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));

        }
        public async Task<IActionResult> Login()
        {
            if(User != null && User.Identity.IsAuthenticated)
            {
                var principal = User as ClaimsPrincipal;
                var emailClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                if(emailClaim != null)
                {
                    var user = await _userManager.FindByEmailAsync(emailClaim.Value);
                    if(user != null && await _userManager.IsSuperAdmin(user))
                        return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            
            if(result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (await _userManager.IsSuperAdmin(user))
                {
                   return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", Messages.USER_NOT_ADMIN);
                return View();
            }

            ModelState.AddModelError("", Messages.INVALID_LOGIN_REQUEST);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home");
        }
    }
}
