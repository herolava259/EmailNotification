

namespace Common.Domain.Generic.EventSourcing;

public class StreamEventWrapper<TStreamEvent>
    where TStreamEvent : BaseSourcingEvent
{
    private ESourcingStreamEventStatus _status;
    public ESourcingStreamEventStatus Status { get => this._status; }

    private ESourcingEventState _state;
    
    public ESourcingEventState State { get => this._state; }

    public TStreamEvent Value { get; private init;}

    private StreamEventWrapper(ESourcingEventState state, ESourcingStreamEventStatus status,TStreamEvent value)
    {
        this._state = state;
        this._status = status;
        Value = value;
    }

    public static StreamEventWrapper<TStreamEvent> Create(ESourcingEventState state,ESourcingStreamEventStatus status, TStreamEvent @event)
        => new StreamEventWrapper<TStreamEvent>(state, status, @event);

    public static StreamEventWrapper<TStreamEvent> Initialize(TStreamEvent @event)
    {
        return new(ESourcingEventState.None, ESourcingStreamEventStatus.Prepare, @event);
    }

    private bool SetState(ESourcingEventState state)
    {
        // define rule to set below
        if (state == ESourcingEventState.None)
            return false;
        if(this._state == ESourcingEventState.Added && 
            !(state == ESourcingEventState.Uncommited 
                || state == ESourcingEventState.Commited))
            return false;
        if (this._state == ESourcingEventState.Commited || this._state == ESourcingEventState.Uncommited)
            return false;

        this._state = state;
        return true;
    }

    private bool SetStatus(ESourcingStreamEventStatus status)
    {

        // define rule to set below
        if(status == ESourcingStreamEventStatus.Prepare)
            return false;
        this._status = status;
        return true;
    }

    public bool SetToAdded()
        => SetState(ESourcingEventState.Added);

    public bool SetToCommited()
        => SetState(ESourcingEventState.Commited);

    public bool SetToUncommited()
        => SetState(ESourcingEventState.Uncommited);

    public bool SwitchToCurrent()
        => SetStatus(ESourcingStreamEventStatus.Current);

    public bool SwithToStale()
        => SetStatus(ESourcingStreamEventStatus.Stale);

}
