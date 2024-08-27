using Cart.Core.Repositories;
using Cart.Infrastructure.Data;
using CartEntity = Cart.Core.Entities.Cart;
namespace Cart.Infrastructure.Repositories
{
    public class CartRepository : BaseRepository<CartEntity>, ICartRepository
    {
        public CartRepository(CartDBContext dbContext) : base(dbContext)
        {
        }
    }
}
