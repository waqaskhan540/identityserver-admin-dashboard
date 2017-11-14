using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResourceController:Controller
    {
        private readonly IResourcerRepository _resourceRepository;
        public ResourceController(IResourcerRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        
        public  IActionResult ApiResources()
        {
            var apiResources = _resourceRepository.GetApiResources().ToList();
            var list = new List<ApiResource>();
            apiResources.ForEach((item) => list.Add(item.ToModel()));

            return View(list);
        }
        
        public IActionResult IdentityResources()
        {
            var identityResources = _resourceRepository.GetIdentityResources().ToList();
            var list = new List<IdentityResource>();
            identityResources.ForEach((item) => list.Add(item.ToModel()));

            return View(list);
        }
    }
}
