using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core.Repositories;

public interface IGeneralRepository: IDisposable
{
    ValueTask<bool> SaveChangeAsync();
}

public interface IGeneralRepository<TEntity>
    where TEntity: EntityBase
{
    Task<(bool, TEntity?)> CreateAsync(TEntity entity);

    Task<TEntity?> FindAsync(Guid id);

    Task<TEntity?> FindOneAsyncWithCondition(Expression<Func<TEntity, bool>> condExpr);

    Task<IEnumerable<TEntity>> GetAll();

    Task<IEnumerable<TEntity>> GetAllAsyncWithCondition(Expression<Func<TEntity, bool>> condExpr);


    Task<(int, int, IEnumerable<TEntity>)> PagingAsyncWithCondition(Expression<Func<TEntity, bool>>? condExpr = null, 
                                                                    int pageNum = 0, int pageSize = 10);
}
