using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Entities;

public enum AgentType: ushort
{
    CrossService = 0,
    Client = 1,
    Worker = 2,
    DataTrigger = 3,
    SchduleTrigger = 4,
}

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool Deleted { get; set; } = false;

}
