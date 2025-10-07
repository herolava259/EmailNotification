using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;

public enum OrderStatus: ushort
{
    Pending = 0,
    Confirmation = 1,
    Preparation = 2, 
    Delivery = 3,
    Received = 4,
}
