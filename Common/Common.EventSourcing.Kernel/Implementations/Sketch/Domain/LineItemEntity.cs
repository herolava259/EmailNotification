using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;

public sealed class LineItemEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}
