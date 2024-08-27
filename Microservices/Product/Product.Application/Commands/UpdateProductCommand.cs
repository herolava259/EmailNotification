using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Commands
{
    public class UpdateProductCommand: IRequest<bool>
    {
        public UpdateProductCommand(Guid id, string productName, decimal price, int quantity)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public string ProductName { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}
