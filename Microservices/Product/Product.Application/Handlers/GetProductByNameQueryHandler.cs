using AutoMapper;
using MediatR;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class GetProductByNameQueryHandler: IRequestHandler<GetProductByNameQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByNameQueryHandler(IProductRepository productRepository,
                                            IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<ProductResponse> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var entitiy = await _productRepository.GetProductByName(request.Name);

            return _mapper.Map<ProductResponse>(entitiy);
        }

        
    }
}
