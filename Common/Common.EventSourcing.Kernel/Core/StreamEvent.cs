
using Common.EventSourcing.Kernel.Generic;

namespace Common.EventSourcing.Kernel.Core;

public enum StreamEventState: ushort
{ 
    Corrected = 0,
    Rejected = 1,
    BiTemporal = 2,
    Duplicated = 3,
    Ignored = 4
}


// read only model 
public sealed partial class StreamEvent<TDomainEvent>: BaseStreamEvent
    where TDomainEvent: IDomainEvent
{
    public TDomainEvent? DomainEvent { get; set; } = default;


}


// For behavior retroactive event 

public sealed partial class StreamEvent<TDomainEvent>: IComparable<StreamEvent<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    public bool WasProcessingError { get; set; } = true;

    private bool _isRejected = false;

    public bool ShouldIgnoreOnReplay
    {
        get
        {
            if (WasProcessingError) return true;

            return _isRejected;
        }
    }
    public bool IsConsequenceOf(StreamEvent<TDomainEvent> other)
        => !ShouldIgnoreOnReplay && this.After(other);

    public bool After(StreamEvent<TDomainEvent> other)
    {
        return this.CompareTo(other) > 0;
    }

    public int CompareTo(StreamEvent<TDomainEvent>? other)
    {
        if (other == null)
            return 1;
        if (this.OccuredOn > other.OccuredOn) return 1;
        else if (this.OccuredOn < other.OccuredOn) return -1;
        else return 0;
    }
}
