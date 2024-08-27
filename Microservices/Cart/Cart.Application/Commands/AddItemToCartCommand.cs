using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Commands
{
    public class AddItemToCartCommand: IRequest<bool>
    {
        public AddItemToCartCommand(string productId, Guid cartId, int amount, decimal price)
        {
            ProductId = productId;
            CartId = cartId;
            Amount = amount;
            Price = price;
        }

        public string ProductId { get; }
        public Guid CartId { get; }
        public int Amount { get; }
        public decimal Price { get; }
    }
}
