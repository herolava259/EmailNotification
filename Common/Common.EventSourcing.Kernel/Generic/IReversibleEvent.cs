using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;

public interface IEventReversible<out TReverseEvent>
    where TReverseEvent: BaseStreamEvent
{
    public TReverseEvent Reverse();
}
