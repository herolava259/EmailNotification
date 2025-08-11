using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductComplexContext;

public sealed partial class ProductComplexAggregate
{

    public IReadOnlyList<ProductPropertyValueObject> Properties { get => this._properties; }

    private readonly List<ProductPropertyValueObject> _properties;
}
