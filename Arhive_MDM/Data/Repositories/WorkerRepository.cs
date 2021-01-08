using Arhive_MDM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateWorker(Worker worker)
        {
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
            return worker.Id;
        }

        public Task<Worker> GetWorker(int workerId)=>
            _context.Workers.FirstOrDefaultAsync(x => x.Id == workerId);
        

        public Task<Worker> GetWorker(string login)=> 
            _context.Workers.FirstOrDefaultAsync(x => x.Login == login);
        

        public Task<List<Worker>> GetWorkers()=>
            _context.Workers.ToListAsync();

        public Task<List<Worker>> GetWorkersWhoRole(string role)=>
            _context.Workers.Where(x => x.Role == role).ToListAsync();

        public Task RemoveWorker(Worker worker)
        {
            _context.Workers.Remove(worker);
            return _context.SaveChangesAsync();
        }

        public Task UpdateWorker(Worker worker)
        {
            _context.Workers.Update(worker);
            return _context.SaveChangesAsync();
        }
    }
}
