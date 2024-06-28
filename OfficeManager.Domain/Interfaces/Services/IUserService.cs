using OfficeManager.Domain.DataObjects.Paginations.Responses;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.DataObjects.Users.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllByOfficeId(Guid officeId);
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(Guid userId);
        Task Create(UserRequest userRequest);
        Task Update(UserRequest userRequest);
        Task<bool> Exists(Guid userId);
        Task Remove(Guid userId);
        Task<UserResponse> LoginAsync(UserRequest userRequest);
        Task ResetPasswordAsync(UserRequest userRequest);
        Task<bool> CheckIfEmailIsAlreadyInUse(string email);
        Task<PaginationResponse<UserResponse>> GetPaginated(UserPaginationRequest request);
    }
}
