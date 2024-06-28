using OfficeManager.Domain.DataObjects.ClientMeetings.Requests;
using OfficeManager.Domain.DataObjects.ClientMeetings.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IClientMeetingService
    {
        Task<List<ClientMeetingResponse>> GetAllByClientId(Guid clientId);
        Task<ClientMeetingResponse> GetById(Guid clientMeetingId);
        Task Create(ClientMeetingRequest clientMeetingRequest);
        Task Update(ClientMeetingRequest clientMeetingRequest);
        Task<List<ClientMeetingResponse>> GetAll();
        Task<bool> Exists(Guid clientMeetingId);
        Task Remove(Guid clientMeetingId);
    }
}
