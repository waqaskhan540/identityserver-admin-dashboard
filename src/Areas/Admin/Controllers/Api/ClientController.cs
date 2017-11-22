using IdentityServer.Interfaces.IdentityServer;
using IdentityServer.Models;
using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
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
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientService clientService,IResourcesRepository resourceRepository,IClientRepository clientRepository)
        {
            _clientService = clientService;
            _clientRepository = clientRepository;
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
        public async Task<IActionResult>  AddClient(IdentityServer4.Models.Client model)
        {
            try
            {
                var client = await _clientService.GetClientById(model.ClientId);
                if(client != null)
                {
                    return new OkObjectResult(BaseModel.Error("Client with specified Id already exists."));
                }

                await  _clientService.AddClient(model);
                return new OkObjectResult(BaseModel.Success("Basic info added",new { client_id = model.ClientId }));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(BaseModel.Error("Operation failed, please try again."));
            }
        }

        [HttpPost("api/client/update")]
        public IActionResult UpdateClient(IdentityServer4.Models.Client model)
        {
            try
            {
                var client = _clientRepository.Get(x => x.ClientId == model.ClientId).FirstOrDefault();
            
                if (client == null)
                    return new OkObjectResult(BaseModel.Error("No client exist with the Id " + model.ClientId));


                var entity  = model.ToEntity();

                client.AllowedGrantTypes = entity.AllowedGrantTypes;
                client.AllowedScopes = entity.AllowedScopes;
                client.AlwaysIncludeUserClaimsInIdToken = entity.AlwaysIncludeUserClaimsInIdToken;
                client.Enabled = entity.Enabled;
                client.AlwaysSendClientClaims = entity.AlwaysSendClientClaims;
                client.AccessTokenLifetime = entity.AccessTokenLifetime;
                client.IdentityTokenLifetime = entity.IdentityTokenLifetime;
                client.AllowOfflineAccess = entity.AllowOfflineAccess;

                _clientRepository.Update(client);

                return new OkObjectResult(BaseModel.Success("Client Added Successfully."));

            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(BaseModel.Error("Operation failed, please try again."));
            }
        }

       

    }
}
