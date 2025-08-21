using Product.Domain.Generic;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductDetailMediaMetadataEntity : MediaMetadataEntity
{
    public override string FileName => throw new NotImplementedException();

    public Guid ProductDetailId { get; set; }
}
