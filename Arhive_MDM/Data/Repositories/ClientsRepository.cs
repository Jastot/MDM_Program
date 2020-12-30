using Arhive_MDM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    class ClientsRepository : IClientsRepository
    {
        private readonly DataContext _context;

        public ClientsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> CreateClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client.Id;
        }

        public Task<Client> GetClient(int clientId) =>
            _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);

        public Task<Client> GetClient(string telephone) =>
            _context.Clients.FirstOrDefaultAsync(x => x.ContactNumber == telephone);

        public Task<List<Client>> GetClients() =>
            _context.Clients.ToListAsync();

        public Task RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
            return _context.SaveChangesAsync();
        }

        public Task UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            return _context.SaveChangesAsync();
        }
    }
}
