using Common.EventSourcing.Kernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;

internal class OrderAggregate : AggregateRoot
{

    public override TResult Apply<TEvent, TResult>(TEvent @event)
    {
        throw new NotImplementedException();
    }

    public override void Apply<TDomainEvent>(TDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
