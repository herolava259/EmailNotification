using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Infrastructure.Data;


namespace Cart.Infrastructure.Repositories;

public class ListItemRepository : BaseRepository<ListItem>, IListItemRepository
{
    public ListItemRepository(ProductDBContext dbContext) : base(dbContext)
    {
    }
}
