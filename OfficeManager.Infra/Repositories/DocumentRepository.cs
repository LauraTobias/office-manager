using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context) { }

        public async Task<List<Document>> GetAllByCaseIdAsync(Guid caseId)
        {
            return await _set
                .Include(x => x.Case)
                .Where(x => x.CaseId == caseId)
                .ToListAsync();
        }

        public async Task<List<Document>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Case)
                .ToListAsync();
        }

        public async Task<Document> GetByIdWithDependencies(Guid documentId)
        {
            return await _set
               .Include(x => x.Case)
               .FirstOrDefaultAsync(x => x.Id == documentId);
        }
    }
}
