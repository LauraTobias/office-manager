using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.DataObjects.Clients.Responses;
using OfficeManager.Domain.DataObjects.Paginations.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<PaginationResponse<ClientResponse>> GetPaginated(ClientPaginationRequest request);
        Task<List<ClientResponse>> GetAllByOfficeId(Guid officeId);
        Task<List<ClientResponse>> GetAll();
        Task<ClientResponse> GetById(Guid clientId);
        Task Create(ClientRequest clientRequest);
        Task Update(ClientRequest clientRequest);
        Task<bool> Exists(Guid clientId);
        Task Remove(Guid clientId);
    }
}
