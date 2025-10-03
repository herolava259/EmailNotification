using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.OrderContext;

public class LineItemEntity
{

    public Guid ProductId { get; set; }

    public uint Quantity { get; set; }

    public decimal PriceLookup { get; set; }


}
