using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Dtos;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IAsyncRepository<Client>
    {
        Task<PaginationQueryResult<Client>> GetPaginated(ClientPaginationRequest request);
        Task<Client> GetByIdWithDependencies(Guid clientId);
        Task<List<Client>> GetAllByOfficeId(Guid officeId);
        Task<List<Client>> GetAllWithDependencies();
    }
}
