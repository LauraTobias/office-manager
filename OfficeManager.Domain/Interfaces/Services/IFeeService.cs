using OfficeManager.Domain.DataObjects.Fees.Requests;
using OfficeManager.Domain.DataObjects.Fees.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IFeeService
    {
        Task<List<FeeResponse>> GetAll();
        Task<FeeResponse> GetById(Guid feeId);
        Task Create(FeeRequest feeRequest);
        Task Update(FeeRequest feeRequest);
        Task<bool> Exists(Guid feeId);
        Task Remove(Guid feeId);
    }
}
