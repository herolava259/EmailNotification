using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Repositories;

public class ListItemRepository : BaseRepository<ListItem>, IListItemRepository
{
    public ListItemRepository(ProductDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> RemoveByCartId(Guid cartId)
    {
        var entities = await _dbContext.ListItems.Where(c => c.Id ==  cartId).ToListAsync();

        _dbContext.RemoveRange();

        return await _dbContext.SaveChangesAsync() == entities.Count;
    }
}
