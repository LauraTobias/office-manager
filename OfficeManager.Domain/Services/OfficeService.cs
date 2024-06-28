using OfficeManager.Domain.DataObjects.Offices.Requests;
using OfficeManager.Domain.DataObjects.Offices.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task Create(OfficeRequest officeRequest)
        {
            var office = Office.BuildWith(officeRequest);

            if (!office.IsValid())
            {
                throw new ArgumentException(string.Join(", ", office.Errors));
            }

            var officeAlreadyExists = await _officeRepository.ExistsAsync(office.Id);

            if (officeAlreadyExists)
            {
                await Update(officeRequest);
                return;
            }

            await _officeRepository.AddAsync(office);

            await _officeRepository.Commit();
        }

        public async Task<bool> Exists(Guid officeId)
        {
            return await _officeRepository.ExistsAsync(officeId);
        }

        public async Task<List<OfficeResponse>> GetAll()
        {
            var result = await _officeRepository.GetAllWithDependencies();

            return result.Select(x => OfficeResponse.BuildWith(x)).ToList();
        }

        public async Task<OfficeResponse> GetById(Guid officeId)
        {
            var result = await _officeRepository.GetByIdWithDependencies(officeId);

            if (result is null)
            {
                throw new KeyNotFoundException("Office not found");
            }

            return OfficeResponse.BuildWith(result);
        }

        public async Task Remove(Guid officeId)
        {
            await _officeRepository.RemoveAsync(officeId);

            await _officeRepository.Commit();
        }

        public async Task Update(OfficeRequest officeRequest)
        {
            var office = Office.BuildWith(officeRequest);

            if (!office.IsValid())
            {
                throw new ArgumentException(string.Join(", ", office.Errors));
            }

            var officeFound = await _officeRepository.GetByIdAsync(office.Id);

            if (officeFound == null)
            {
                await Create(officeRequest);
                return;
            }

            officeFound.Update(office);

            await _officeRepository.UpdateAsync(officeFound);

            await _officeRepository.Commit();
        }
    }
}
