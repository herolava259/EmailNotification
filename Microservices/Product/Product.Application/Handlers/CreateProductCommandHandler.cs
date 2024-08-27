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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newEntity = new ProductEntity
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
            };

            var result = await _productRepository.CreateProduct(newEntity);

            return result;
        }
    }
}
