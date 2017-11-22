using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Dashboard.Areas.Admin.ViewComponents
{
    [ViewComponent]
    public class ClientList : ViewComponent
    {
        private readonly IClientRepository _clientRepository;
        public ClientList(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var clients = _clientRepository.Get().ToList();
            var list = new List<IdentityServer4.Models.Client>();
            clients.ForEach((ele) => list.Add(ele.ToModel()));
           
            return View(list);
        }
    }
}
