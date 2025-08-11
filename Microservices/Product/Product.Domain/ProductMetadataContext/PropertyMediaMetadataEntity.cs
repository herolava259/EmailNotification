using Product.Domain.Generic;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class PropertyMediaMetadataEntity : MediaMetadataEntity
{
    public override string FileName => throw new NotImplementedException();

    public Guid PropertyId { get; set; }
}
