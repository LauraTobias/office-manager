using OfficeManager.Domain.DataObjects.Offices.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Responses;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOfficeService _officeService;

        public UserService(
            IUserRepository userRepository, 
            IOfficeService officeService)
        {
            _userRepository = userRepository;
            _officeService = officeService;
        }

        public async Task Create(UserRequest userRequest)
        {
            var user = User.BuildWith(userRequest);

            if (!user.IsValid())
            {
                throw new ArgumentException(string.Join(", ", user.Errors));
            }

            await CheckIfDependenciesExist(user);

            var userAlreadyExists = await _userRepository.ExistsAsync(user.Id);

            if (userAlreadyExists)
            {
                await Update(userRequest);
                return;
            }

            await _userRepository.AddAsync(user);

            await _userRepository.Commit();
        }

        public async Task<bool> CheckIfEmailIsAlreadyInUse(string email)
        {
            return await _userRepository.CheckIfEmailIsAlreadyInUse(email);
        }

        public async Task<bool> Exists(Guid userId)
        {
            return await _userRepository.ExistsAsync(userId);
        }

        public async Task<List<UserResponse>> GetAll()
        {
            var result = await _userRepository.GetAllWithDependencies();

            return result.Select(x => UserResponse.BuildWith(x)).ToList();
        }

        public async Task<UserResponse> GetById(Guid userId)
        {
            var result = await _userRepository.GetByIdWithDependencies(userId);

            if (result is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return UserResponse.BuildWith(result);
        }

        public async Task Remove(Guid userId)
        {
            await _userRepository.RemoveAsync(userId);

            await _userRepository.Commit();
        }

        public async Task Update(UserRequest userRequest)
        {
            var user = User.BuildWith(userRequest);

            if (!user.IsValid())
            {
                throw new ArgumentException(string.Join(", ", user.Errors));
            }

            var userFound = await _userRepository.GetByIdAsync(user.Id);

            if (userFound == null)
            {
                await Create(userRequest);
                return;
            }

            await CheckIfDependenciesExist(user);

            userFound.Update(user);

            await _userRepository.UpdateAsync(userFound);

            await _userRepository.Commit();
        }

        public async Task<UserResponse> LoginAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetByEmailAsync(userRequest.Email);

            if (user is null || !user.VerifyPassword(userRequest.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            return UserResponse.BuildWith(user);
        }

        public async Task ResetPasswordAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetByEmailAsync(userRequest.Email);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.ResetPassword(userRequest.Password);

            await _userRepository.UpdateAsync(user);

            await _userRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(User user)
        {
            var officeExists = await _officeService.Exists(user.OfficeId);

            if (!officeExists)
            {
                throw new KeyNotFoundException("Office not found");
            }
        }

        public async Task<PaginationResponse<UserResponse>> GetPaginated(UserPaginationRequest request)
        {
            var usersPaginated = await _userRepository.GetPaginated(request);

            var usersResponses = usersPaginated.QueriedItems.Select(x => UserResponse.BuildWith(x));

            return new PaginationResponse<UserResponse>(usersResponses, request.Page, request.Page, usersPaginated.TotalItemCount);
        }

        public async Task<List<UserResponse>> GetAllByOfficeId(Guid officeId)
        {
            var result = await _userRepository.GetAllByOfficeId(officeId);

            return result.Select(x => UserResponse.BuildWith(x)).ToList();
        }
    }
}
