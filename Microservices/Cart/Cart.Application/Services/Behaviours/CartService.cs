using Cart.Application.Services.Interfaces;
using Cart.Application.Commands;
using Cart.Application.Queries;
using Cart.Application.Responses;
using Product.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cart.Application.Services.Behaviours;

public class CartService : ICartService
{
    private readonly ProductProtoService.ProductProtoServiceClient _productGrpcClient;
    private readonly IMediator _mediator;
    private readonly ILogger<CartService> _logger;

    public CartService(ProductProtoService.ProductProtoServiceClient productGrpcClient,
                       IMediator mediator,
                       ILogger<CartService> logger)
    {
        this._productGrpcClient = productGrpcClient;
        this._mediator = mediator;
        this._logger = logger;
    }
    public async Task<bool> AddItemToCart(AddItemToCartCommand command)
    {

        _logger.LogDebug("Enter {method} method", nameof(AddItemToCart));

        var product = await _productGrpcClient.GetProductAsync
                                (new GetProductRequest() { ProductId = command.ProductId });

        if(product is null)
        {
            _logger.LogError("Cannot find product from product service with id= {ProductId}", command.ProductId);
            return false;
        }
        var currCart = await _mediator.Send(new GetCartByIdQuery(command.CartId));

        if (currCart is null) {
            _logger.LogError("Cannot find cart with Id= {CartId}", command.CartId);
            return false;
        }
        
        if(!ValidateTotalAmoutWareHouseOfItemAvailable(product, command, currCart))
        {
            _logger.LogError("Amount of product's not enough in warehouse");
            return false;
        }
        _logger.LogDebug("Leave {method} method.", nameof(AddItemToCart));
        return await _mediator.Send(command);
    }

    private bool ValidateTotalAmoutWareHouseOfItemAvailable(ProductModel product, 
                                                AddItemToCartCommand command,
                                                CartResponse cart)
    {
        var amountOfItemInCart = command.Amount;
        amountOfItemInCart += cart.ListItems!.Where(c => c.ProductId == product.Id)
                                             .Aggregate(0, (result, nxt) =>
                                             {
                                                 result += nxt.Amount;
                                                 return result;
                                             });

        return product.Quantity >= amountOfItemInCart;
    }

    public async Task<Guid> CreateCart(CreateCartCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<bool> DecreaseItemsInCart(DecreaseItemInCartCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<bool> DeleteCartById(Guid id)
        => await _mediator.Send(new DeleteCartByIdCommand(id));

    public async Task<IList<CartResponse>> GetAllCart(GetAllCartQuery cartQuery)
        => await _mediator.Send(cartQuery);

    public async Task<CartResponse> GetCartById(Guid id)
        => await _mediator.Send(new GetCartByIdQuery(id));

    public async Task<ListItemResponse> GetListItemById(Guid id)
        => await _mediator.Send(new GetListItemByIdQuery(id));

    public async Task<bool> RemoveItemFromCart(RemoveItemFromCartCommand command)
        => await _mediator.Send(command);
}
