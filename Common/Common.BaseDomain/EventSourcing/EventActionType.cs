using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.EventSourcing;

public enum EventActionType: ushort
{
    Create = 0,
    Deleted = 1,
    Updated = 2,
    PartialUpdated = 3,
    None = 4,
}
