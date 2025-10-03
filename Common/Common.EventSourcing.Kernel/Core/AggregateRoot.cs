
using Common.EventSourcing.Kernel.Generic;

namespace Common.EventSourcing.Kernel.Core;

public abstract class AggregateRoot: IBoundedContext
{
    public abstract record DomainResult {

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public bool HasNew { get; set; } = false;

    }

    public abstract TResult Apply<TEvent, TResult>(TEvent @event)
        where TResult: DomainResult
        where TEvent: IDomainEvent;

    public abstract void Apply<TDomainEvent>(TDomainEvent @event) 
        where TDomainEvent : IDomainEvent;

    public Guid Id { get; protected init; } = Guid.Empty;

    public Guid AppliedEventId { get; set; } = Guid.Empty;

}
