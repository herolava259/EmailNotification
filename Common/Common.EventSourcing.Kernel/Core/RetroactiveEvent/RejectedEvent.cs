using Common.EventSourcing.Kernel.Generic;

namespace Common.EventSourcing.Kernel.Core.RetroactiveEvent;

public sealed class RejectEvent<TRejectedDomainEvent>: BaseStreamEvent
    where TRejectedDomainEvent: IDomainEvent
{
    public StreamEvent<TRejectedDomainEvent>? RejectedEvent { get; set; }
}
