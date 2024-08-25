using Cart.Application.Commands;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handlers
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IListItemRepository _listItemRepository;

        public RemoveItemFromCartCommandHandler(ICartRepository cartRepository,
                                                IListItemRepository listItemRepository)
        {
            this._cartRepository = cartRepository;
            this._listItemRepository = listItemRepository;
        }

        

        public async Task<bool> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var removingItem = await _listItemRepository.GetAsync(c => c.Id == request.ListItemId, tracked: false);

            if (removingItem is null)
                return false;

            var cartOfItem = await _cartRepository.GetAsync(c => c.Id == removingItem.CartId, tracked: false);

            if (cartOfItem!.TotalPrice < removingItem.Amount * request.PriceOfItem)
                return false;

            cartOfItem.TotalPrice -= removingItem.Amount * request.PriceOfItem;

            var check = await _listItemRepository.RemoveAsync(removingItem);

            if (!check)
                return false;

            return await _cartRepository.UpdateAsync(cartOfItem);

        }
    }
}
