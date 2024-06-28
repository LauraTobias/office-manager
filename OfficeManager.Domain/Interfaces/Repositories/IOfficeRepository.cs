using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IOfficeRepository : IAsyncRepository<Office>
    {
        Task<Office> GetByIdWithDependencies(Guid officeId);
        Task<List<Office>> GetAllWithDependencies();
    }
}
