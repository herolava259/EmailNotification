using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Commands
{
    public class CreateProductCommand: IRequest<bool>
    {
        public CreateProductCommand(string productName, decimal price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}
