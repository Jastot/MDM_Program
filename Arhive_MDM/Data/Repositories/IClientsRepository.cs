using Arhive_MDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    interface IClientsRepository
    {
        /// <summary>Gets client.</summary>
        /// <param name="clientId">Id of the worker.</param>
        /// <returns>Returns worker.</returns>
        Task<Client> GetClient(int clientId);

        /// <summary>Gets client.</summary>
        /// <param name="telephone">Login of the client.</param>
        /// <returns>Returns client.</returns>
        Task<Client> GetClient(string telephone);

        /// <summary>Gets all clients.</summary>
        /// <returns>Returns all clients.</returns>
        Task<List<Client>> GetClients();

        /// <summary>Creates client.</summary>
        /// <param name="client">client data.</param>
        /// <returns>Returns id of the new client.</returns>
        Task<int> CreateClient(Client client);

        /// <summary>Updates client.</summary>
        /// <param name="client">client data.</param>
        Task UpdateClient(Client client);

        /// <summary>Removes client.</summary>
        /// <param name="client">client data.</param>
        Task RemoveClient(Client client);
    }
}
