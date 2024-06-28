using OfficeManager.Domain.DataObjects.Offices.Requests;
using OfficeManager.Domain.DataObjects.Offices.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IOfficeService
    {
        Task<List<OfficeResponse>> GetAll();
        Task<OfficeResponse> GetById(Guid officeId);
        Task Create(OfficeRequest officeRequest);
        Task Update(OfficeRequest officeRequest);
        Task<bool> Exists(Guid officeId);
        Task Remove(Guid officeId);
    }
}
