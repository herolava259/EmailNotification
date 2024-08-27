
using Cart.Core.Entities;

namespace Cart.Core.Repositories;

public interface IListItemRepository: IBaseRepository<ListItem>
{
    Task<bool> RemoveByCartId(Guid cartId);
}
