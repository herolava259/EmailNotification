using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;


public interface IDomainEvent { }

public interface IDomainEvent<TAggregateRoot>: IDomainEvent
    where TAggregateRoot: IBoundedContext
{
    public string AggregateId { get; }

    public string AggregateType { get; }

    TAggregateRoot HandleLogic(TAggregateRoot aggregate);
}
