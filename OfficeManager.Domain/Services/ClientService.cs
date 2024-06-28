using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.DataObjects.Clients.Responses;
using OfficeManager.Domain.DataObjects.Paginations.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Responses;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOfficeService _officeService;

        public ClientService(
            IClientRepository clientRepository, 
            IOfficeService officeService)
        {
            _clientRepository = clientRepository;
            _officeService = officeService;
        }
        public async Task Create(ClientRequest clientRequest)
        {
            var client = Client.BuildWith(clientRequest);

            if (!client.IsValid())
            {
                throw new ArgumentException(string.Join(", ", client.Errors));
            }

            await CheckIfDependenciesExist(client);

            var clientAlreadyExists = await _clientRepository.ExistsAsync(client.Id);

            if (clientAlreadyExists)
            {
                await Update(clientRequest);
                return;
            }

            await _clientRepository.AddAsync(client);

            await _clientRepository.Commit();
        }

        public async Task<bool> Exists(Guid clientId)
        {
            return await _clientRepository.ExistsAsync(clientId);
        }

        public async Task<List<ClientResponse>> GetAll()
        {
            var result = await _clientRepository.GetAllWithDependencies();

            return result.Select(x => ClientResponse.BuildWith(x)).ToList();
        }

        public async Task<List<ClientResponse>> GetAllByOfficeId(Guid officeId)
        {
            var result = await _clientRepository.GetAllByOfficeId(officeId);

            return result.Select(x => ClientResponse.BuildWith(x)).ToList();
        }

        public async Task<ClientResponse> GetById(Guid clientId)
        {
            var result = await _clientRepository.GetByIdWithDependencies(clientId);

            if (result is null)
            {
                throw new KeyNotFoundException("Client not found");
            }

            return ClientResponse.BuildWith(result);
        }

        public async Task<PaginationResponse<ClientResponse>> GetPaginated(ClientPaginationRequest request)
        {
            var clientsPaginated = await _clientRepository.GetPaginated(request);

            var clientsResponses = clientsPaginated.QueriedItems.Select(x => ClientResponse.BuildWith(x));

            return new PaginationResponse<ClientResponse>(clientsResponses, request.Page, request.Page, clientsPaginated.TotalItemCount);
        }

        public async Task Remove(Guid clientId)
        {
            await _clientRepository.RemoveAsync(clientId);

            await _clientRepository.Commit();
        }

        public async Task Update(ClientRequest clientRequest)
        {
            var client = Client.BuildWith(clientRequest);

            if (!client.IsValid())
            {
                throw new ArgumentException(string.Join(", ", client.Errors));
            }

            var clientFound = await _clientRepository.GetByIdAsync(client.Id);

            if (clientFound == null)
            {
                await Create(clientRequest);
                return;
            }

            await CheckIfDependenciesExist(client);

            clientFound.Update(client);

            await _clientRepository.UpdateAsync(clientFound);

            await _clientRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(Client client)
        {
            var officeExists = await _officeService.Exists(client.OfficeId);

            if (!officeExists)
            {
                throw new KeyNotFoundException("Office not found");
            }
        }
    }
}
