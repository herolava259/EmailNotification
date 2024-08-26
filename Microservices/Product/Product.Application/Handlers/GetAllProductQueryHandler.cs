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
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<IList<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var entities = await _productRepository.GetAllProduct();

            return _mapper.Map<IList<ProductResponse>>(entities);
        }
    }
}
