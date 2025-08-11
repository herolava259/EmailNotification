using Product.Domain.ProductMetadataContext.Enumerations;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductPropertyPriceValueEntity
{
    #region propeties
    public decimal Price { get; private init; } = 0;

    public PriceCurrencyType Currency { get; private init; } = PriceCurrencyType.USD;

    public Guid ProductMetadataId { get; private init; } = Guid.Empty;

    #endregion

    #region constructors, static create methods
    #endregion
}
