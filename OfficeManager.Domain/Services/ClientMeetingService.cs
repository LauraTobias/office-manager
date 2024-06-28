using OfficeManager.Domain.DataObjects.ClientMeetings.Requests;
using OfficeManager.Domain.DataObjects.ClientMeetings.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class ClientMeetingService : IClientMeetingService
    {
        private readonly IClientMeetingRepository _clientMeetingRepository;
        private readonly IClientService _clientService;

        public ClientMeetingService(
            IClientMeetingRepository clientMeetingRepository,
            IClientService clientService)
        {
            _clientMeetingRepository = clientMeetingRepository;
            _clientService = clientService;
        }

        public async Task Create(ClientMeetingRequest clientMeetingRequest)
        {
            var clientMeeting = ClientMeeting.BuildWith(clientMeetingRequest);

            if (!clientMeeting.IsValid())
            {
                throw new ArgumentException(string.Join(", ", clientMeeting.Errors));
            }

            await CheckIfDependenciesExist(clientMeeting);

            var clientMeetingAlreadyExists = await _clientMeetingRepository.ExistsAsync(clientMeeting.Id);

            if (clientMeetingAlreadyExists)
            {
                await Update(clientMeetingRequest);
                return;
            }

            await _clientMeetingRepository.AddAsync(clientMeeting);

            await _clientMeetingRepository.Commit();
        }

        public async Task<bool> Exists(Guid clientMeetingId)
        {
            return await _clientMeetingRepository.ExistsAsync(clientMeetingId);
        }

        public async Task<List<ClientMeetingResponse>> GetAll()
        {
            var result = await _clientMeetingRepository.GetAllWithDependencies();

            return result.Select(x => ClientMeetingResponse.BuildWith(x)).ToList();
        }

        public async Task<List<ClientMeetingResponse>> GetAllByClientId(Guid clientId)
        {
            var result = await _clientMeetingRepository.GetAllByClientId(clientId);

            return result.Select(x => ClientMeetingResponse.BuildWith(x)).ToList();
        }

        public async Task<ClientMeetingResponse> GetById(Guid clientMeetingId)
        {
            var result = await _clientMeetingRepository.GetByIdWithDependencies(clientMeetingId);

            if (result is null)
            {
                throw new KeyNotFoundException("Client meeting not found");
            }

            return ClientMeetingResponse.BuildWith(result);
        }

        public async Task Remove(Guid clientMeetingId)
        {
            await _clientMeetingRepository.RemoveAsync(clientMeetingId);

            await _clientMeetingRepository.Commit();
        }

        public async Task Update(ClientMeetingRequest clientMeetingRequest)
        {
            var clientMeeting = ClientMeeting.BuildWith(clientMeetingRequest);

            if (!clientMeeting.IsValid())
            {
                throw new ArgumentException(string.Join(", ", clientMeeting.Errors));
            }

            var clientMeetingFound = await _clientMeetingRepository.GetByIdAsync(clientMeeting.Id);

            if (clientMeetingFound == null)
            {
                await Create(clientMeetingRequest);
                return;
            }

            await CheckIfDependenciesExist(clientMeeting);

            clientMeetingFound.Update(clientMeeting);

            await _clientMeetingRepository.UpdateAsync(clientMeetingFound);

            await _clientMeetingRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(ClientMeeting clientMeeting)
        {
            var clientExists = await _clientService.Exists(clientMeeting.ClientId);

            if (!clientExists)
            {
                throw new KeyNotFoundException("Client not found");
            }
        }
    }
}
