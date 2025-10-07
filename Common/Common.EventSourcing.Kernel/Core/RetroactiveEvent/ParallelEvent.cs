
namespace Common.EventSourcing.Kernel.Core.RetroactiveEvent;

public class ParallelEvent<TEvent>: BaseStreamEvent
    where TEvent: BaseStreamEvent, new()
{

    public TEvent? Event { get; set; }
}
