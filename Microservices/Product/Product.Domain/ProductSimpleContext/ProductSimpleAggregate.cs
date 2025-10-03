
namespace Product.Domain.ProductSimpleContext;

public sealed partial class ProductSimpleAggregate
{
    public string Name { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string ImageFile { get; set; }


    public ProductBrandEntity Brand { get; set; }

    public ProductTypeEntity Type { get; set; }

    public decimal Price { get; set; }


}
