using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;

public interface IRewindable
{
    Task RewindBetween<TEvent>(TEvent @beginEvent, TEvent @endEvent)
        where TEvent: StreamEvent;

    Task RewindTo<TEvent>(TEvent @event)
        where TEvent: StreamEvent;

    Task RewindAfter<TEvent>(TEvent @event)
        where TEvent: StreamEvent;


}

public interface IRebuildEvent
{
    Task<TAggregateRoot> RebuildAsync<TAggregateRoot>();
}

public interface IReversible
{
    Task<bool> ReverseAsync<TEvent>(TEvent @event);
}

public interface IMergeable
{
    Task<bool> MergeAsync(BranchVersion versionA, BranchVersion versionB);

}

public interface IEventAsyncProcessor<TAggregateRoot>
    where TAggregateRoot : IBoundedContext
{
    public AggregateSnapshot<TAggregateRoot> AggregateSnapshot { get;  }


    Task ProceedAsync<TEvent>(TEvent @event)
        where TEvent: StreamEvent;


}
