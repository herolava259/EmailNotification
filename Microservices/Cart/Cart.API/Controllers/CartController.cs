using Cart.Application.Commands;
using Cart.Application.Queries;
using Cart.Application.Responses;
using Cart.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cart.API.Controllers
{
    public class CartController: ApiController
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            this._cartService = cartService;
            this._logger = logger;
        }

        [HttpGet("getcartbyid/{cartId}", Name = "GetCartById")]
        [ProducesResponseType(typeof(CartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CartResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CartResponse>> GetCartById(Guid cartId)
        {
            var result = await _cartService.GetCartById(cartId);

            if(result is null)
            {
                _logger.LogError($"Cannot cart with Id = {cartId}");
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("getlistitembyid/{listItemId}", Name = "GetListItemById")]
        [ProducesResponseType(typeof(ListItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ListItemResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ListItemResponse>> GetListItemById(Guid listItemId)
        {
            var result = await _cartService.GetListItemById(listItemId);

            if (result is null)
            {
                _logger.LogError($"Cannot cart with Id = {listItemId}");
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("getallcart", Name = "GetAllCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IList<CartResponse>>> GetAllCart([FromBody] GetAllCartQuery getAllCartQuery)
        {
            var result = await _cartService.GetAllCart(getAllCartQuery);


            return Ok(result);
        }

        [HttpPost("createcart", Name = "CreateCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> CreateCart([FromBody] CreateCartCommand command)
        {
            var result = await _cartService.CreateCart(command);

            if(result == Guid.Empty)
            {
                _logger.LogError("Cannot create cart");
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("deletecart/{cartId}", Name = "DeleteCart")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCart(Guid cartId)
        {
            var result = await _cartService.DeleteCartById(cartId);

            if (!result)
            {
                _logger.LogError($"Cannot delete cart by id {cartId}");
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost("additemtocart", Name = "AddItemCart")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddItemToCart([FromBody] AddItemToCartCommand command)
        {
            var result = await _cartService.AddItemToCart(command);

            if(!result)
            {
                _logger.LogError("Cannot add more item to this cart");
                return BadRequest();
            }


            return Accepted();
        }

        [HttpPost("decreaseitemincart", Name = "DecreaseItemsInCart")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DecreaseItemsInCart([FromBody] DecreaseItemInCartCommand command)
        {
            var result = await _cartService.DecreaseItemsInCart(command);

            if (!result)
            {
                _logger.LogError("Cannot decrease item in this cart");
                return BadRequest();
            }


            return Accepted();
        }

        [HttpPost("removeitemfromcart", Name = "RemoveItemFromCart")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveItemFromCart([FromBody] RemoveItemFromCartCommand command)
        {
            var result = await _cartService.RemoveItemFromCart(command);

            if (!result)
            {
                _logger.LogError("Cannot remove item from this cart");
                return BadRequest();
            }


            return Accepted();
        }
    }
}
