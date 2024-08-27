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
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, bool>
    {
        private readonly IListItemRepository _listItemRepository;
        private readonly ICartRepository _cartRepository;

        public AddItemToCartCommandHandler(IListItemRepository listItemRepository, 
                                            ICartRepository cartRepository)
        {
            this._listItemRepository = listItemRepository;
            this._cartRepository = cartRepository;
        }
        public async Task<bool> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var currCart = await _cartRepository.GetAsync(c => c.Id == request.CartId,
                                                    tracked: false,
                                                    includeProperties: "ListItems");
            if (currCart is null)
                return false;

            var choosenItem = currCart.ListItems!.FirstOrDefault(c => c.ProductId == request.ProductId);

            if(choosenItem is null)
            {
                var newItemId = await _listItemRepository.CreateAsync(new()
                {
                    Amount = request.Amount,
                    CartId = request.CartId,
                    ProductId = request.ProductId,
                });

                if (newItemId == Guid.Empty) return false;

            }
            else
            {
                choosenItem.Amount += request.Amount;

                var check = await _listItemRepository.UpdateAsync(choosenItem);

                if (!check) return false;

            }

            currCart.TotalPrice += request.Amount * request.Price;

            return await _cartRepository.PartialUpdateAsync(currCart, new() { "TotalPrice" });

        }
    }
}
