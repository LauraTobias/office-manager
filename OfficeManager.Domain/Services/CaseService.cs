using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.DataObjects.Cases.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IClientService _clientService;

        public CaseService(
            ICaseRepository caseRepository, 
            IClientService clientService)
        {
            _caseRepository = caseRepository;
            _clientService = clientService;
        }
        public async Task Create(CaseRequest caseRequest)
        {
            var caseEntity = Case.BuildWith(caseRequest);

            if (!caseEntity.IsValid())
            {
                throw new ArgumentException(string.Join(", ", caseEntity.Errors));
            }

            await CheckIfDependenciesExist(caseEntity);

            var caseAlreadyExists = await _caseRepository.ExistsAsync(caseEntity.Id);

            if (caseAlreadyExists)
            {
                await Update(caseRequest);
                return;
            }

            await _caseRepository.AddAsync(caseEntity);

            await _caseRepository.Commit();
        }

        public async Task<bool> Exists(Guid caseId)
        {
            return await _caseRepository.ExistsAsync(caseId);
        }

        public async Task<List<CaseResponse>> GetAll()
        {
            var result = await _caseRepository.GetAllWithDependencies();

            return result.Select(x => CaseResponse.BuildWith(x)).ToList();
        }

        public async Task<List<CaseResponse>> GetAllWithFilters(CaseRequest caseRequest)
        {
            var result = await _caseRepository.GetAllWithFilters(caseRequest);

            return result.Select(x => CaseResponse.BuildWith(x)).ToList();
        }

        public async Task<CaseResponse> GetById(Guid caseId)
        {
            var result = await _caseRepository.GetByIdWithDependencies(caseId);

            if (result is null)
            {
                throw new KeyNotFoundException("Case not found");
            }

            return CaseResponse.BuildWith(result);
        }

        public async Task Remove(Guid caseId)
        {
            await _caseRepository.RemoveAsync(caseId);

            await _caseRepository.Commit();
        }

        public async Task Update(CaseRequest caseRequest)
        {
            var newCase = Case.BuildWith(caseRequest);

            if (!newCase.IsValid())
            {
                throw new ArgumentException(string.Join(", ", newCase.Errors));
            }

            var caseFound = await _caseRepository.GetByIdAsync(newCase.Id);

            if (caseFound == null)
            {
                await Create(caseRequest);
                return;
            }

            await CheckIfDependenciesExist(newCase);

            caseFound.Update(newCase);

            await _caseRepository.UpdateAsync(caseFound);

            await _caseRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(Case caseEntity)
        {
            var clientExists = await _clientService.Exists(caseEntity.ClientId);

            if (!clientExists)
            {
                throw new KeyNotFoundException("Client not found");
            }
        }
    }
}
