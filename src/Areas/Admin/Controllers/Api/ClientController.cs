using IdentityServer.Interfaces.IdentityServer;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Areas.Admin.Controllers.Api
{
   // [Authorize]
    [Route("api/[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

       [HttpGet]
        public BaseModel  UpdateClientStatus(string clientId,bool enableStatus)
        {
            try
            {
                var result = _clientService.SetClientStatus(clientId, enableStatus);
                if(result)
                {
                    string status = enableStatus == true ? "Enabled" : "Disabled";
                    return BaseModel.Success($"Client {clientId} {status}");
                }
                return BaseModel.Error("Client not found");

            }
            catch (Exception)
            {
                return BaseModel.Error("Operation failed, please try again.");
            }
        }
        [HttpPost("api/client/add")]
        public async Task<IActionResult>  AddClient(IdentityServer4.Models.Client client)
        {
            try
            {
                await  _clientService.AddClient(client);
                return new OkObjectResult(BaseModel.Success("Basic info added"));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Error("Operation failed, please try again."));
            }
        }

    }
}
