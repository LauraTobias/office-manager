using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Enums;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(DbContext context) : base(context) { }

        public async Task<List<Case>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Client)
                .ToListAsync();
        }

        public async Task<List<Case>> GetAllWithFilters(CaseRequest caseRequest)
        {
            var query = _set.Include(x => x.Client).AsQueryable();

            if (!string.IsNullOrEmpty(caseRequest.Name))
                query = query.Where(d => d.Name.StartsWith(caseRequest.Name));

            if (caseRequest.Status is not null)
                query = query.Where(d => d.Status == caseRequest.Status);

            return await query.ToListAsync();
        }

        public async Task<Case> GetByIdWithDependencies(Guid caseId)
        {
            return await _set
               .Include(x => x.Client)
               .FirstOrDefaultAsync(x => x.Id == caseId);
        }
    }
}
