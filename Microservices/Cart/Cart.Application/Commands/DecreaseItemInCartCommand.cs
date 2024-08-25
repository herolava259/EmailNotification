using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Commands
{
    public class DecreaseItemInCartCommand: IRequest<bool>
    {
        public DecreaseItemInCartCommand(string productId, Guid cartId, int decreasedAmount, decimal price)
        {
            ProductId = productId;
            CartId = cartId;
            DecreasedAmount = decreasedAmount;
            Price = price;
        }

        public string ProductId { get; }
        public Guid CartId { get; }
        public int DecreasedAmount { get; }
        public decimal Price { get; }
    }
}
