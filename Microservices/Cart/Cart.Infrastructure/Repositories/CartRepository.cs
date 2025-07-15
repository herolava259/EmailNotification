using Cart.Core.Repositories;
using Cart.Infrastructure.Data;
using System.Threading.Tasks;
using CartEntity = Cart.Core.Entities.Cart;
namespace Cart.Infrastructure.Repositories
{
    public class CartRepository : BaseRepository<CartEntity>, ICartRepository
    {
        public CartRepository(CartDBContext dbContext) : base(dbContext)
        {
        }

        private async Task<IEnumerable<CartEntity>> GetOrderedCartEntities(string name)
        {
            return await Task.FromResult(Enumerable.Empty<CartEntity>());
        }
    }
}
