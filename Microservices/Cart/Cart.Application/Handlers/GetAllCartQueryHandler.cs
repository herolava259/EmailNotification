using AutoMapper;
using Cart.Application.Queries;
using Cart.Application.Responses;
using Cart.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartQuery, IList<CartResponse>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(ICartRepository cartRepository,
                                      IMapper mapper)
        {
            this._cartRepository = cartRepository;
            this._mapper = mapper;
        }
        public async Task<IList<CartResponse>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
        {
            var entities = await _cartRepository.GetAllAsync(includeProperties: request.IncludeItem ? "ListItems" : null,
                                                             pageNumber: request.PageIndex,
                                                             pageSize: request.PageIndex);
            return _mapper.Map<IList<CartResponse>>(entities);
        }
    }
}
