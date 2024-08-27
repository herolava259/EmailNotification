using EmailNotification.Core.Entities;
using EmailNotification.Core.Repositories;
using EmailNotification.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmailNotification.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly EmailNotificationDBContext _dbContext;

    public BaseRepository(EmailNotificationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> condition)
    {
        return await _dbContext.Set<T>().AnyAsync(condition);
    }

    public async Task<Guid> CreateAsync(T entity)
    {
        entity.Id = Guid.NewGuid();

        _dbContext.Set<T>().Add(entity);

        if((await _dbContext.SaveChangesAsync()) > 0)
        {
            return entity.Id;
        }

        return Guid.Empty;
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
    {
        var entityQuery = _dbContext.Set<T>().AsNoTracking();

        if(filter != null)
        {
            entityQuery = entityQuery.Where(filter);
        }

        if (pageSize > 0)
        {
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            entityQuery = entityQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                entityQuery = entityQuery.Include(includeProp);

            }
        }

        return await entityQuery.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
    {
        var dbSet = _dbContext.Set<T>();
        var query = dbSet.AsNoTracking();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> PartialUpdateAsync(T entity, List<string> modifyParams)
    {
        _dbContext.Attach(entity);
        var entry = _dbContext.Entry(entity);
        modifyParams.ForEach(c =>
        {
            entry.Property(c).IsModified = true;
        });

        return await _dbContext.SaveChangesAsync() > 0;
    }
    

    public async Task<bool> RemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
