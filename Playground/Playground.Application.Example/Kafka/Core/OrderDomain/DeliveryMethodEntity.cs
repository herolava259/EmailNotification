using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.OrderDomain;

public class DeliveryMethodEntity: EntityBase
{
    public string ShortName { get; set; }
    public string DeliveryTime { get; set; }

    public string Description { get; set; }

    public decimal TotalPrice { get; set; }
}
