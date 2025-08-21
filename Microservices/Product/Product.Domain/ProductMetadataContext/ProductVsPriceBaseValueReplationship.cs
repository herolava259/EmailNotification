using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductVsPriceBaseValueReplationship
{
    public Guid ProductId { get; set; }

    public Guid ProductPriceValueId { get; set; }
}
