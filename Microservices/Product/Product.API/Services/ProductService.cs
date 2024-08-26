using Grpc.Core;
using MediatR;
using Product.Application.Commands;
using Product.Application.Queries;
using Product.Grpc.Protos;

namespace Product.API.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public override async Task<ProductModel> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var query = new GetProductByIdQuery(new Guid(request.ProductId));

            var result = await _mediator.Send(query);

            return result;
        }

        public override async Task<ProductModel> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var product = request.Product;
            var cmd = new CreateProductCommand(product.ProductName, product.Price, product.Quantity);

            var result = await _mediator.Send(cmd);

            if (!result)
                return default!;

            var newProductResp = await _mediator.Send(new GetProductByNameQuery(product.ProductName));

            if (newProductResp == null)
                return default!;

            return newProductResp;
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var cmd = new RemoveProductCommand(new Guid(request.ProductId));

            var result = await _mediator.Send(cmd);

            return new DeleteProductResponse
            {
                Success = result
            };
        }

        public override async Task<ProductModel> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var product = request.Product;
            var cmd = new UpdateProductCommand(new Guid(product.Id),
                                               product.ProductName,
                                               product.Price,
                                               product.Quantity);

            var result = await _mediator.Send(cmd);

            if (!result)
                return default!;

            var updatedProduct = await _mediator.Send(new GetProductByIdQuery(new Guid(product.Id)));

            if (updatedProduct == null) return default!;

            return updatedProduct;
        }
    }
}
