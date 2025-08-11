using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductPriceProductPropertyRelatioship
{
    public Guid ProductPropertyId { get; private init; } = Guid.Empty;

    public Guid ProductPriceValueId { get; set; } = Guid.Empty;


}
