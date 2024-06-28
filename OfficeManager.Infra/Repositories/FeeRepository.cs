using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        public FeeRepository(DbContext context) : base(context) { }

        public async Task<Fee> GetByIdWithDependencies(Guid feeId)
        {
            return await _set
                .Include(x => x.Case)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == feeId);
        }

        public async Task<List<Fee>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Case)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
