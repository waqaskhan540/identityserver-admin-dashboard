using IdentityServer.Interfaces.IdentityServer;
using IdentityServer.Repositories.Interfaces.IdentityServerRepositories;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityServer.Services.IdentityServer
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task AddClient(Client client)
        {
            await _clientRepository.AddAsync(client.ToEntity());            
        }

        public bool SetClientStatus(string clientId, bool enableStatus)
        {
            try
            {
                var client = _clientRepository.Get(x => x.ClientId == clientId).FirstOrDefault();
                if(client != null)
                {
                    client.Enabled = enableStatus;
                    _clientRepository.Update(client);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
