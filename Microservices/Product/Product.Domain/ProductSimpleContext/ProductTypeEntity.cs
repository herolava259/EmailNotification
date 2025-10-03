using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductSimpleContext;

public sealed partial class ProductTypeEntity
{
    public string Name { get; set; }

    public Guid ProductAggregateId { get; set; }
}
