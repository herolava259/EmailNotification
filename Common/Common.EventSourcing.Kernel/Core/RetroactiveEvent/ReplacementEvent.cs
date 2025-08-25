using Common.EventSourcing.Kernel.Generic;

namespace Common.EventSourcing.Kernel.Core.RetroactiveEvent;

public enum RetroactiveActionType: ushort
{
    OutOfOrder = 0,
    Incorrect = 1,
    Rejected = 2,
}

public class ReplacementEvent<TOriginalEvent, TReplacementEvent>: BaseStreamEvent
    where TOriginalEvent: IDomainEvent
    where TReplacementEvent : IDomainEvent
{
    public string OriginalId { get; protected init; }
    public string ReplacementId { get; protected init; }

    public StreamEvent<TOriginalEvent> Original { get; set; }

    public StreamEvent<TReplacementEvent> Replacement { get; set; }

    public ReplacementEvent(StreamEvent<TOriginalEvent> originalEvent, StreamEvent<TReplacementEvent> replacementEvent)
    {   
        Original = originalEvent;
        Replacement = replacementEvent;
        OriginalId = originalEvent.IdempotenceKey;
        ReplacementId = replacementEvent.IdempotenceKey;
    }

}
