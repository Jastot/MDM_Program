using Arhive_MDM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    class DocumentsRepository : IDocumentsRepository
    {
        private readonly DataContext _context;

        public DocumentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateDocuments(Documents document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document.Id;
        }

        public Task<Documents> GetDocuments(int documentId)=>
            _context.Documents.FirstOrDefaultAsync(x => x.Id == documentId);
  

        public Task<List<Documents>> GetDocuments()=>
            _context.Documents.ToListAsync();

        public Task<List<Documents>> GetDocumentsByTime(DateTime dataFrom, DateTime dataTo) =>
            _context.Documents.Where(x => x.TimeCreated >= dataFrom && x.TimeCreated <= dataTo).ToListAsync();


        public Task RemoveDocuments(Documents document)
        {
            _context.Documents.Remove(document);
            return _context.SaveChangesAsync();
        }

        public Task UpdateDocuments(Documents document)
        {
            _context.Documents.Update(document);
            return _context.SaveChangesAsync();
        }
    }
}
