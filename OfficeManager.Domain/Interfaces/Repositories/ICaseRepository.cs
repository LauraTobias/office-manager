using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface ICaseRepository : IAsyncRepository<Case>
    {
        Task<List<Case>> GetAllWithFilters(CaseRequest caseRequest);
        Task<Case> GetByIdWithDependencies(Guid caseId);
        Task<List<Case>> GetAllWithDependencies();
    }
}
