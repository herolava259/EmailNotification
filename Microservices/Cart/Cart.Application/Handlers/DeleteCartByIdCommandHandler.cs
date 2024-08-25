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
    public class DeleteCartByIdCommandHandler : IRequestHandler<DeleteCartByIdCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IListItemRepository _listItemRepository;

        public DeleteCartByIdCommandHandler(ICartRepository cartRepository,
                                            IListItemRepository listItemRepository)
        {
            this._cartRepository = cartRepository;
            this._listItemRepository = listItemRepository;
        }
        public async Task<bool> Handle(DeleteCartByIdCommand request, CancellationToken cancellationToken)
        {
            if (!(await _listItemRepository.RemoveByCartId(request.CartId)))
                return false;

            var entity = await _cartRepository.GetAsync(c => c.Id == request.CartId);

            if (entity == null) return false;

            return await _cartRepository.RemoveAsync(entity);
        }
    }
}
