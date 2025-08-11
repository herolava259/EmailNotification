using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductDetailMetadataEntity
{
    public IReadOnlyList<ProductCommonPropertyValueObject> CommonPropeties { get => this._commonProperties; }
    private readonly List<ProductCommonPropertyValueObject> _commonProperties = new();



}
