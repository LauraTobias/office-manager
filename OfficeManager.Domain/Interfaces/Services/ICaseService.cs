using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.DataObjects.Cases.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface ICaseService
    {
        Task<List<CaseResponse>> GetAllWithFilters(CaseRequest caseRequest);
        Task<CaseResponse> GetById(Guid caseId);
        Task Create(CaseRequest caseRequest);
        Task Update(CaseRequest caseRequest);
        Task<List<CaseResponse>> GetAll();
        Task<bool> Exists(Guid caseId);
        Task Remove(Guid caseId);
    }
}
