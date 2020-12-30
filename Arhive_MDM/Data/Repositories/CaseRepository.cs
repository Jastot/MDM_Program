using Arhive_MDM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    class CaseRepository : ICaseRepository
    {
        private readonly DataContext _context;

        public CaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCase(Case Case)
        {
            await _context.Cases.AddAsync(Case);
            await _context.SaveChangesAsync();
            return Case.Id;
        }

        public Task<Case> GetCase(int caseId) =>
            _context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);

        public Task<List<Case>> GetCases() =>
            _context.Cases.ToListAsync();

        public Task<List<Case>> GetWorkerCases(int workerId) =>
            _context.Cases.Where(x => x.WorkerId == workerId).ToListAsync();

        public Task RemoveCase(Case Case)
        {
            // Нельзя удалить, если есть документы. понял.
            _context.Cases.Remove(Case);
            return _context.SaveChangesAsync();
        }

        public Task UpdateCase(Case Case)
        {
            _context.Cases.Update(Case);
            return _context.SaveChangesAsync();
        }
    }
}
