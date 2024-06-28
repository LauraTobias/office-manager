using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.DataObjects.Paginations.Dtos;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Interfaces.Repositories;

namespace OfficeManager.Infra.Repositories
{
    public class Repository<TEntity> : IAsyncRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext _context;

        protected readonly DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
        }
        
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _set.AnyAsync(x => x.Id == id);
        }


        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public Task RemoveAsync(TEntity entity)
        {
            _set.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            foreach (Guid id in ids)
            {
                TEntity entity = await _set.FindAsync(id);
                _set.Remove(entity);
            }
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            TEntity exist = await _set.FindAsync(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
        }

        public static async Task<PaginationQueryResult<TEntity>> ApplyPagination(IOrderedQueryable<TEntity> query, int page, int itemsByPage)
        {
            var skip = (page - 1) * itemsByPage;

            if (skip < 0)
                skip = 0;

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip(skip)
                .Take(itemsByPage)
                .ToListAsync();

            return new PaginationQueryResult<TEntity>(items, totalItems);
        }
    }
}