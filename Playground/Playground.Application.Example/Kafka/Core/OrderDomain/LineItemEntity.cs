using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.OrderDomain;

public class LineItemEntity: EntityBase
{
    public LineItemEntity()
    {
        
    }

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice 
        => Quantity * Price;
}
