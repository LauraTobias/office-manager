using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        public OfficeRepository(DbContext context) : base(context) { }

        public async Task<Office> GetByIdWithDependencies(Guid officeId)
        {
            return await _set
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == officeId);
        }

        public async Task<List<Office>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Users)
                .ToListAsync();
        }
    }
}
