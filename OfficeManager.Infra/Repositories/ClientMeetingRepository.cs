using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class ClientMeetingRepository : Repository<ClientMeeting>, IClientMeetingRepository
    {
        public ClientMeetingRepository(DbContext context) : base(context) { }

        public async Task<List<ClientMeeting>> GetAllByClientId(Guid clientId)
        {
            return await _set
                .Include(x => x.Client)
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<List<ClientMeeting>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Client)
                .ToListAsync();
        }

        public async Task<ClientMeeting> GetByIdWithDependencies(Guid clientMeetingId)
        {
            return await _set
               .Include(x => x.Client)
               .FirstOrDefaultAsync(x => x.Id == clientMeetingId);
        }
    }
}
