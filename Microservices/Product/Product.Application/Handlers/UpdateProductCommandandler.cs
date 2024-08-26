using AutoMapper;
using MediatR;
using Product.Application.Commands;
using Product.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEntity = Product.Core.Entities.Product;

namespace Product.Application.Handlers
{
    public class UpdateProductCommandandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandandler(IProductRepository productRepository,
                                          IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatingEntity = _mapper.Map<ProductEntity>(request);

            return await _productRepository.UpdateProduct(updatingEntity);
        }
    }
}
