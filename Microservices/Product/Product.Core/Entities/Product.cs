
namespace Product.Core.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
