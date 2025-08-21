using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;


public enum ProductDetailStatus: ushort
{
    Available = 0,
    Unavailable = 1
}

public sealed partial class ProductDetailMetadataEntity
{
    public Guid ProductMetadataId { get; private init; } = Guid.Empty;
    public IReadOnlyList<ProductCommonPropertyValueObject> CommonPropeties { get => this._commonProperties; }
    private readonly List<ProductCommonPropertyValueObject> _commonProperties = new();

    public ProductDetailStatus Status { get; set; }

    public Guid BrandId { get; set; }

    public TimeSpan WanrrantyPeriod { get; private set; } = TimeSpan.Zero;


    public string? Description { get; set; }



}
