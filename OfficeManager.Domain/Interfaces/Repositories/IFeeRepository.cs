using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IFeeRepository : IAsyncRepository<Fee>
    {
        Task<Fee> GetByIdWithDependencies(Guid feeId);
        Task<List<Fee>> GetAllWithDependencies();
    }
}
