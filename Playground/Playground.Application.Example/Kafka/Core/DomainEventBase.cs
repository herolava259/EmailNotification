using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core;

public interface IDomaniEvent { }

public abstract record DomainEventBase<TAggregateRoot>(Guid AggregateId, DateTimeOffset OccuredTime): IDomaniEvent
    where TAggregateRoot: IAggregateRoot
{
    
}
