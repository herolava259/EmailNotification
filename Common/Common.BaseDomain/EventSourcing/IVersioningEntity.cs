using Common.Domain.Generic.EventSourcing;

namespace Common.BaseDomain.EventSourcing;

public interface IEventLoader<in TStreamEvent>
    where TStreamEvent: BaseSourcingEvent
{
    bool Load(IEnumerable<TStreamEvent> events);

    bool Add(TStreamEvent streamEvent);
}

public interface ICommitEvent<TStreamEvent>
    where TStreamEvent: BaseSourcingEvent
{
    ValueTask<bool> CommitAsync<TEventStore>(TEventStore store)
        where TEventStore: IEventStore<TStreamEvent>;
}

public interface ISwitchState
{
    void Switch(string branchVersion);

    ValueTask<bool> SwitchAsync<TEventStore>(TEventStore eventStore, string brachVersion);
}

public interface IReplayEvent<TStreamEvent>
    where TStreamEvent: BaseSourcingEvent
{
    bool Replay();
    bool Replay(IEnumerable<TStreamEvent> events);

    ValueTask<bool> ReplayAsync<TEventStore>(TEventStore eventStore, string branchVersion)
        where TEventStore : IEventStore<TStreamEvent>;
}

public interface IApplyEvent<in TStreamEvent>
    where TStreamEvent: BaseSourcingEvent
{
    bool Apply(TStreamEvent @event);

}

public interface IRejectEvent
{
    bool RejectAll();

    bool RejectFrom(string brachVersion, ulong versionNo);

    bool RejectFrom(ulong versionNo);
}

public interface IVersioningEntity<TStreamEvent> : IEventLoader<TStreamEvent>, ICommitEvent<TStreamEvent>, ISwitchState,
    IReplayEvent<TStreamEvent>, IApplyEvent<TStreamEvent>
    where TStreamEvent: BaseSourcingEvent
{
    public IReadOnlyCollection<TStreamEvent> AppliedEvents { get; }

    public IReadOnlyCollection<TStreamEvent> UncommitedEvents { get; }
    ulong VersionNo { get; }

    public string BranchVersion { get; }


}
