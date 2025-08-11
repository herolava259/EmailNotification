
namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductPropertyEntity
{
    public string Title { get; set; }

    public string Name { get; set; }

    public Guid PropetyGroupId { get; private init; } = Guid.Empty;

    public IReadOnlySet<PropertyAttributeObject> Attributes { get => _properties; }
    private readonly HashSet<PropertyAttributeObject> _properties = new HashSet<PropertyAttributeObject>();

    public IReadOnlyList<PropertyMediaMetadataEntity> MediaMetadatas { get => _mediaMetadatas; }
    private readonly List<PropertyMediaMetadataEntity> _mediaMetadatas = new();
}
