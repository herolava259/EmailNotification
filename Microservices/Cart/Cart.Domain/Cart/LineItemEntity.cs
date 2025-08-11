using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Cart;

public class LineItemEntity
{
    public Guid ProductId { get; private init; } = Guid.Empty;

    public uint Quantity { get; set; } = 0;

    public LineItemLookupObject LineItemInformation { get; private init; }
}
