using Microsoft.EntityFrameworkCore;
using Playground.JobFinder.Data;
using Playground.JobFinder.Modules;
using System.Linq.Expressions;

namespace Playground.JobFinder.Supports.Repositories;

public class SupportRepository<TEntity>: ISupportRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly ApplicationDbContext _db;


    public SupportRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _db.Set<TEntity>().AddAsync(entity);
        await SaveAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
    {

        var query = _db.Set<TEntity>().AsQueryable();
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null,
                                                                      int pageSize = 0, int pageNumber = 1)
    {
        var query = _db.Set<TEntity>().AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (pageSize > 0)
        {
            pageSize = Math.Min(pageSize, 100);

            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return await query.ToListAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _db.Set<TEntity>().Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<bool> SaveChangeAsync()
    {
        return (await _db.SaveChangesAsync()) > 0;
    }

    public async Task<bool> SaveChangeAsync(TEntity entity, ModifyAction actionType)
    {
        switch(actionType){
            case ModifyAction.Create:
                _db.Set<TEntity>().Add(entity);
                break;
            case ModifyAction.Update:
                _db.Set<TEntity>().Update(entity);
                break;
            case ModifyAction.Delete:
                _db.Set<TEntity>().Remove(entity);
                break;
        }

        return await SaveChangeAsync();
    }
}
