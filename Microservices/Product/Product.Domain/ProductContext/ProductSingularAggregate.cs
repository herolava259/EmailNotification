using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductContext;

public sealed partial class ProductSingularAggregate
{

    public string Name { get; private init; } = String.Empty;
    public string Description { get; private init; } = String.Empty;

    public decimal Price { get; private init; } = 0;

    // media information 
    public string MediaUri { get; private set; } = String.Empty;

    public string ImageThumbUri { get; private set; } = String.Empty;


    public Guid MechantId { get; private set; } = Guid.Empty;

    public string ProductDetail { get; private set; } = String.Empty;

    public string ProductDescription { get; private set; } = String.Empty;

}
