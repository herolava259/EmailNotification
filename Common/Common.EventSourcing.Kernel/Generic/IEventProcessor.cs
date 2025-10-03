using Common.EventSourcing.Kernel.Core;
using System.Linq.Expressions;

namespace Common.EventSourcing.Kernel.Generic;

public interface IRewindable
{
    Task RewindBetween<TEvent>(TEvent @beginEvent, TEvent @endEvent)
        where TEvent: BaseStreamEvent;

    Task RewindTo<TEvent>(TEvent @event)
        where TEvent: BaseStreamEvent;

    Task RewindAfter<TEvent>(TEvent @event)
        where TEvent : BaseStreamEvent;

}

public interface IRebuildable
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

public interface IAsyncEventProcessor<TAggregateRoot>
    where TAggregateRoot : IBoundedContext
{

    public Task<TResult> ProceedDomainEventAsync<TDomainEvent, TResult>(StreamEvent<TDomainEvent> @event)
        where TDomainEvent: IDomainEvent<TAggregateRoot>;

}

public interface IEventProcessorBuilder<TAggregateRoot>
    where TAggregateRoot : IBoundedContext
{
    public enum SourcingSessionType: ushort
    {
        Production = 0,
        Debugging = 1,
        Auditing = 2,
        Other = 3
    }

    #region infrastructure
    IEventProcessorBuilder<TAggregateRoot> HasEventStore();

    IEventProcessorBuilder<TAggregateRoot> UseSnapshotStorage();

    IEventProcessorBuilder<TAggregateRoot> WithCachingEvent();

    IEventProcessorBuilder<TAggregateRoot> PublishAsyncProjectionTo();

    #endregion

    #region behaviour 
    IEventProcessorBuilder<TAggregateRoot> AcceptParallel();
    IEventProcessorBuilder<TAggregateRoot> WithProceedConflictScheme();

    // transactional behavior

    // distributed transaction when modification session 

    // need to a mechanism to flexibility to handle or make some rule to ingress event 
    #endregion


    IEventProcessorBuilder<TAggregateRoot> WithAggregateId(string aggregateId);

    IEventProcessorBuilder<TAggregateRoot> ForSession(SourcingSessionType sessionType);

    //IEventProcessorBuilder<TAggregateRoot> WithTagWhenProcessing(string key, string value);

    IEventProcessorBuilder<TAggregateRoot> ShouldRebuildAll(bool shouldRebuildAll = true);

    IEventProcessorBuilder<TAggregateRoot> HasQueryFilter(Expression<Predicate<BaseStreamEvent>> queryExpr);

    Task<IAsyncEventProcessor<TAggregateRoot>> BuildAsync();
}
