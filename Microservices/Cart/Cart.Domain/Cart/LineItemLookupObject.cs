using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Cart;

public sealed record LineItemLookupObject
{
    public string ProductName { get; private init; } = String.Empty;

    public Guid MerchantId { get; private init; } = Guid.Empty;

    public string MerchantName { get; private init; } = String.Empty;

    public string CategoryType { get; set; } = String.Empty;

    public uint StockQuantity { get; set; } = 0;

    public decimal Price { get; set; } = 0;

    public string Currency { get; private init; } = String.Empty;

    public string CategoryInformation { get; private init; } = String.Empty;

    public string BrandName { get; private init; } = String.Empty;

    public Guid BrandTypeId { get; private init; } = Guid.Empty;

    public string Description { get; private init; } = String.Empty;



}
