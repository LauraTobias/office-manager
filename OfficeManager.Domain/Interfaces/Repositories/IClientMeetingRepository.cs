using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IClientMeetingRepository : IAsyncRepository<ClientMeeting>
    {
        Task<ClientMeeting> GetByIdWithDependencies(Guid clientMeetingId);
        Task<List<ClientMeeting>> GetAllByClientId(Guid clientId);
        Task<List<ClientMeeting>> GetAllWithDependencies();
    }
}
