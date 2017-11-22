using IdentityServer.Dashboard.Areas.Admin.ViewComponents;
using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ClientsController:Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IResourcesRepository _resourceRepository;
        public ClientsController(IClientRepository clientRepository,IResourcesRepository resourceRepository)
        {
            _clientRepository = clientRepository;
            _resourceRepository = resourceRepository;
        }
        public IActionResult Index()
        {
           

            ViewBag.ApiResources = _resourceRepository.GetApiResources().ToList().Select(x => x.Name);
            ViewBag.IdentityResources = _resourceRepository.GetIdentityResources().ToList().Select(x => x.Name);
           
            return View();
        }

        public IActionResult GetClients()
        {
            return ViewComponent(nameof(ClientList));
        }
    }
}
