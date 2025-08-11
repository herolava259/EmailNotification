using Product.Domain.ProductMetadataContext.Enumerations;

namespace Product.Domain.Generic;

public abstract class MediaMetadataEntity
{
    public string ObjectId { get; protected init; } = String.Empty;
    public string ExposedName { get; protected set; } = String.Empty;
    public string Description { get; protected set; } = String.Empty;

    public MediaMetadataType Type { get; protected init; } = MediaMetadataType.None;

    public string RandomName { get; protected init; } = String.Empty;

    public abstract string FileName { get;}

    public string ClusterId { get; protected init; } = String.Empty;

    public string LogicalPath { get; protected init; } = String.Empty;


}
