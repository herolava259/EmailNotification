using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductComplexContext;

public sealed partial class ProductToProductRelationship
{
    public Guid ParentProductId { get; private init; } = Guid.Empty;

    public Guid AttachedProductId { get; set; } = Guid.Empty;
}
