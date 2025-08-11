
using Common.BaseDomain.EventSourcing;

namespace Common.Domain.Generic.EventSourcing;


//public interface IChangeEntity<in TApplyEvent>
//    where TApplyEvent : IApplyEvent

//{
//    void ChangeEntity(TApplyEvent entity);
        
//}

public abstract record BaseSourcingEvent
{
    public string VersionBranch { get; protected init; } = "V0";

    public DateTimeOffset TimeStamp { get; protected init; } = DateTimeOffset.UtcNow;

    public EventActionType Action { get; protected init; } = EventActionType.None;

    public string EmmittedSourceId { get; protected init; } = String.Empty;

    public EventSourceType SourceType { get; protected init; } = EventSourceType.Request;

    public Guid CorrelationId { get; protected init; } = Guid.Empty;

    
}
