using Cart.Application.Commands;
using Cart.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers
{
    public class DecreaseItemInCartCommandHandler: IRequestHandler<DecreaseItemInCartCommand, bool>
    {
        private readonly IListItemRepository _listItemRepository;
        private readonly ICartRepository _cartRepository;

        public DecreaseItemInCartCommandHandler(IListItemRepository listItemRepository,
                                            ICartRepository cartRepository)
        {
            this._listItemRepository = listItemRepository;
            this._cartRepository = cartRepository;
        }

        public async Task<bool> Handle(DecreaseItemInCartCommand request, CancellationToken cancellationToken)
        {
            var currCart = await _cartRepository.GetAsync(c => c.Id == request.CartId,
                                                    tracked: false,
                                                    includeProperties: "ListItems");
            if (currCart is null)
                return false;

            var choosenItem = currCart.ListItems!.FirstOrDefault(c => c.ProductId == request.ProductId);

            if (choosenItem is null)
            {

                return false;
            }
            else
            {
                if (choosenItem.Amount < request.DecreasedAmount)
                    return false;
                choosenItem.Amount -= request.DecreasedAmount;
                var check = true;
                if (choosenItem.Amount == 0)
                    check = await _listItemRepository.RemoveAsync(choosenItem);
                else 
                    check = await _listItemRepository.UpdateAsync(choosenItem);

                if (!check) return false;

            }

            currCart.TotalPrice -= request.DecreasedAmount * request.Price;

            return await _cartRepository.PartialUpdateAsync(currCart, new() { "TotalPrice" });

        }
    }
}
