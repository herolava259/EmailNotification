using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public abstract record BaseDomainEvent
{
    public string EventType { get; private init; } = String.Empty;
}
