using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.OrderContext;

public sealed partial class OrderAggregate
{
    public string OrderNo { get; private init; } = "001";

    public uint Amount { get; private set; } = 1;

    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;


}
