using OfficeManager.Domain.DataObjects.Fees.Requests;
using OfficeManager.Domain.DataObjects.Fees.Responses;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;
        private readonly ICaseService _caseService;
        private readonly IUserService _userService;

        public FeeService(
            IFeeRepository repository, 
            ICaseService caseService, 
            IUserService userService)
        {
            _feeRepository = repository;
            _caseService = caseService;
            _userService = userService;
        }
        public async Task Create(FeeRequest feeRequest)
        {
            var fee = Fee.BuildWith(feeRequest);

            if (!fee.IsValid())
            {
                throw new ArgumentException(string.Join(", ", fee.Errors));
            }

            await CheckIfDependenciesExist(fee);

            var feeAlreadyExists = await _feeRepository.ExistsAsync(fee.Id);

            if (feeAlreadyExists)
            {
                await Update(feeRequest);
                return;
            }

            await _feeRepository.AddAsync(fee);

            await _feeRepository.Commit();
        }

        public async Task<bool> Exists(Guid feeId)
        {
            return await _feeRepository.ExistsAsync(feeId);
        }

        public async Task<List<FeeResponse>> GetAll()
        {
            var result = await _feeRepository.GetAllWithDependencies();

            return result.Select(x => FeeResponse.BuildWith(x)).ToList();
        }

        public async Task<FeeResponse> GetById(Guid feeId)
        {
            var result = await _feeRepository.GetByIdWithDependencies(feeId);

            if (result is null)
            {
                throw new KeyNotFoundException("Fee not found");
            }

            return FeeResponse.BuildWith(result);
        }

        public async Task Remove(Guid feeId)
        {
            await _feeRepository.RemoveAsync(feeId);

            await _feeRepository.Commit();
        }

        public async Task Update(FeeRequest feeRequest)
        {
            var fee = Fee.BuildWith(feeRequest);

            if (!fee.IsValid())
            {
                throw new ArgumentException(string.Join(", ", fee.Errors));
            }

            var feeFound = await _feeRepository.GetByIdAsync(fee.Id);

            if (feeFound == null)
            {
                await Create(feeRequest);
                return;
            }

            await CheckIfDependenciesExist(fee);

            feeFound.Update(fee);

            await _feeRepository.UpdateAsync(feeFound);

            await _feeRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(Fee fee)
        {
            var caseExists = await _caseService.Exists(fee.CaseId);

            if (!caseExists)
            {
                throw new KeyNotFoundException("Case not found");
            }

            var userExists = await _userService.Exists(fee.UserId);

            if (!userExists)
            {
                throw new KeyNotFoundException("User not found");
            }
        }
    }
}
