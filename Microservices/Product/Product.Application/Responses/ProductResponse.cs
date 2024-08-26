using Product.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public static implicit operator ProductModel(ProductResponse response)
            => response == null ? default : response.ToProductModel();
        public ProductModel ToProductModel()
            => new ProductModel
            {
                Id = Id.ToString(),
                ProductName = ProductName,
                Price = Price,
                Quantity = Quantity
            };
    }
}
