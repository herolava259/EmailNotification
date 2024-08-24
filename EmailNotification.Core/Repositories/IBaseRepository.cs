using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Repositories
{
    public interface IBaseRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1);

        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
        Task<Guid> CreateAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> condition);
        Task<bool> RemoveAsync(T entity);

        Task SaveAsync();

        Task<bool> PartialUpdateAsync(T entity, List<string> modifyParams);

        
    }
}
