using OfficeManager.Domain.Entities.EntityBases;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : EntityBase
    {
        Task Commit();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> ExistsAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IList<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(params Guid[] ids);
        Task RemoveAsync(TEntity entity);
    }
}