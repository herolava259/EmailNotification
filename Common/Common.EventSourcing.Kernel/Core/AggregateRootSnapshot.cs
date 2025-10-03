using Common.EventSourcing.Kernel.Generic;

namespace Common.EventSourcing.Kernel.Core;

public interface IBoundedContext
{
    public void Apply<TDomainEvent>(TDomainEvent @event)
        where TDomainEvent : IDomainEvent;
}





public abstract class AggregateSnapshot<TBoundedContext>
    where TBoundedContext: IBoundedContext
{
    public IDomainEvent? CurrentEvent { get; set; }

    public BranchVersion Version { get; private set; } = BranchVersion.Empty;

    public abstract TBoundedContext AggregateRoot { get; }



}
