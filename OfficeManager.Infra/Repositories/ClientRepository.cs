using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Dtos;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DbContext context) : base(context) { }

        public async Task<Client> GetByIdWithDependencies(Guid clientId)
        {
            return await _set
                .Include(x => x.Office)
                .Include(x => x.Meetings)
                .FirstOrDefaultAsync(x => x.Id == clientId);
        }

        public async Task<List<Client>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Office)
                .Include(x => x.Meetings)
                .ToListAsync();
        }

        public async Task<PaginationQueryResult<Client>> GetPaginated(ClientPaginationRequest request)
        {
            var query = _set
                .Include(x => x.Office)
                .Include(x => x.Meetings)
                .Where(x => x.OfficeId == request.OfficeId);

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.StartsWith(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Cpf))
            {
                query = query.Where(x => x.Cpf.StartsWith(request.Cpf));
            }

            return await ApplyPagination(query.OrderBy(x => x.Name), request.Page, request.Size);
        }

        public async Task<List<Client>> GetAllByOfficeId(Guid officeId)
        {
            return await _set
                .Include(x => x.Office)
                .Include(x => x.Meetings)
                .Where(x => x.OfficeId == officeId)
                .ToListAsync();
        }
    }
}
