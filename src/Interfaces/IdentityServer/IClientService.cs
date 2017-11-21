
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces.IdentityServer
{
    public interface IClientService
    {
        bool SetClientStatus(string clientId, bool enableStatus);
        Task AddClient(Client client);
        Task<Client> GetClientById(string Id);
        void Update(Client client);
    }
}
