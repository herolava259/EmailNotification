using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public interface IDomainEventProcessor<TDomainEvent>
    where TDomainEvent: BaseDomainEvent
{
    IReadOnlyList<TDomainEvent> Events { get; }
    void RaiseEvent(TDomainEvent @event);
}
