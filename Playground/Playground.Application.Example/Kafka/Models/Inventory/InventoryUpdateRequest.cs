using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Models.Inventory;

public sealed class InventoryUpdateRequest: BaseRequest
{
    public Guid EntityId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    // TODO: brainstorm use case and declare business logic prop later
}
