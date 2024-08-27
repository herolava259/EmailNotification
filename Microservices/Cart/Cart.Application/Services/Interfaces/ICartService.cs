using Cart.Application.Commands;
using Cart.Application.Queries;
using Cart.Application.Responses;

namespace Cart.Application.Services.Interfaces;

public interface ICartService
{
    Task<Guid> CreateCart(CreateCartCommand command);

    Task<CartResponse> GetCartById(Guid id);

    Task<ListItemResponse> GetListItemById(Guid id);

    Task<IList<CartResponse>> GetAllCart(GetAllCartQuery cartQuery);

    Task<bool> DeleteCartById(Guid id);

    Task<bool> AddItemToCart(AddItemToCartCommand command);

    Task<bool> DecreaseItemsInCart(DecreaseItemInCartCommand command);

    Task<bool> RemoveItemFromCart(RemoveItemFromCartCommand command);

}
