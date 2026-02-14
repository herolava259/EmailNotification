using Playground.JobFinder.Models.DTOs;
using System.Linq.Expressions;

namespace Playground.JobFinder.Bases;

public interface IAdvancedSupportRepository
{
    Task<bool> SaveChangeAsync(); 
}

public interface IAdvancedSupportRepository<TEntity>
    where TEntity: EntityBase
{
    Task<(bool, TEntity?)> Add(TEntity dto, bool refresh = false);

    Task<(bool, TEntity?)> Update(TEntity dto, bool refresh = false);

    Task<bool> Delete(Guid id, bool softDelete = true);

    Task<TEntity?> FindById(Guid id);

    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>>? condExpr = null);

    Task<IEnumerable<TData>> GetAllTransform<TData>(Expression<Func<TEntity, bool>>? condExpr = null, Expression<Func<TEntity, TData>>? selector = null);


    Task<bool> Exists(Guid id);

    Task<bool> Exists(Expression<Func<TEntity, bool>> condExpr);

    Task<(int, int, IEnumerable<TEntity>)> Paging(int pageIndex, int pageSize, string? includeProperties = null);


    Task<int> Count();

    Task<int> Count(Expression<Func<TEntity, bool>> condExpr);
}
