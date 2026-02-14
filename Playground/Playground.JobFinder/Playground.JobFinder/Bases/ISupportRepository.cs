using System.Linq.Expressions;

namespace Playground.JobFinder.Bases;

public interface ISupportRepository<TEntity>: IGeneralDomainRepository<TEntity>
    where TEntity : EntityBase
{
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null,
                            int pageSize = 0, int pageNumber = 1);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
    Task CreateAsync(TEntity entity);

    Task RemoveAsync(TEntity entity);

    Task SaveAsync();
}
