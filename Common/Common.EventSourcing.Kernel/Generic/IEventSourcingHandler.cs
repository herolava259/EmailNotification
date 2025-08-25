using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;


// proceed cross domain logic when some decisions need external information or other domain logic
// has business logic
// For ingress event, the event as behavior logic
// process business logic 
public interface IEventSourcingHandler<in TStreamEvent>
    where TStreamEvent: BaseStreamEvent
{
    void Handle(TStreamEvent @event);
}

public interface IAsyncEventSourcingHandler<in TStreamEvent>
    where TStreamEvent : BaseStreamEvent
{
    Task HandleAsync(TStreamEvent @event);
}
