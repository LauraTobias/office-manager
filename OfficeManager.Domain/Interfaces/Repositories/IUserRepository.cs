using OfficeManager.Domain.DataObjects.Paginations.Dtos;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<PaginationQueryResult<User>> GetPaginated(UserPaginationRequest request);
        Task<bool> CheckIfEmailIsAlreadyInUse(string email);
        Task<List<User>> GetAllByOfficeId(Guid officeId);
        Task<User> GetByIdWithDependencies(Guid userId);
        Task<List<User>> GetAllWithDependencies();
        Task<User> GetByEmailAsync(string email);
    }
}
