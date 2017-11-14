using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
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
        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public IActionResult Index()
        {
            var clients = _clientRepository.Get().ToList();
            var list = new List<IdentityServer4.Models.Client>();
            clients.ForEach((ele) => list.Add(ele.ToModel()));

            return View(list);
        }
    }
}
