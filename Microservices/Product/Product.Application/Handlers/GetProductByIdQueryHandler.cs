
using AutoMapper;
using MediatR;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Repositories;

namespace Product.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository,
                                      IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.GetProductById(request.Id);

        return _mapper.Map<ProductResponse>(entity);
    }
}
