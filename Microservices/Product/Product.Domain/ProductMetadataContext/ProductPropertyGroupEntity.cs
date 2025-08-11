using Product.Domain.ProductMetadataContext.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;



public sealed partial class ProductPropertyGroupEntity
{
    public string Name { get; private init; } = String.Empty;

    public IReadOnlyList<ProductPropertyEntity> PropertyValues { get => this._propertyValues; }

    private readonly List<ProductPropertyEntity> _propertyValues = new();

    public PropertyGroupType Type { get; private init; } = PropertyGroupType.Exclude;



}
