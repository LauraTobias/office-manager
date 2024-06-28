using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.DataObjects.Paginations.Dtos;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public async Task<User> GetByIdWithDependencies(Guid userId)
        {
            return await _set
                .Include(x => x.Office)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<List<User>> GetAllWithDependencies()
        {
            return await _set
                .Include(x => x.Office)
                .ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _set
               .Include(x => x.Office)
               .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> CheckIfEmailIsAlreadyInUse(string email)
        {
            return await _set.AnyAsync(x => x.Email == email);
        }

        public async Task<PaginationQueryResult<User>> GetPaginated(UserPaginationRequest request)
        {
            var query = _set
                .Include(x => x.Office)
                .Where(x => x.OfficeId == request.OfficeId);

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.StartsWith(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(x => x.Email.StartsWith(request.Email));
            }

            return await ApplyPagination(query.OrderBy(x => x.Name), request.Page, request.Size);
        }

        public async Task<List<User>> GetAllByOfficeId(Guid officeId)
        {
            return await _set
                .Include(x => x.Office)
                .Where(x => x.OfficeId == officeId)
                .ToListAsync();
        }
    }
}
