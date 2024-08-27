using MediatR;
using Product.Application.Commands;
using Product.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public RemoveProductCommandHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.DeleteProduct(request.ProductId);
            return result;
        }
    }
}
