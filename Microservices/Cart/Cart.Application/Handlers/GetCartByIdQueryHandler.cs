using AutoMapper;
using Cart.Application.Queries;
using Cart.Application.Responses;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handlers
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartByIdQueryHandler(ICartRepository cartRepository, IMapper mapper)
        {
            this._cartRepository = cartRepository;
            this._mapper = mapper;
        }

        public async Task<CartResponse> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _cartRepository.GetAsync(c => c.Id == request.Id, tracked: false,
                                            includeProperties: "ListItems");
            if (entity is null) return default!;
            return _mapper.Map<CartResponse>(entity);
        }

        
    }
}
