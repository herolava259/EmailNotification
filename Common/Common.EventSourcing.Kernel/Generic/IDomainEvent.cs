using Common.EventSourcing.Kernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Generic;


public interface IDomainEvent { }

public interface IDomainEvent<TAggregateRoot>: IDomainEvent, IStreamEvent
    where TAggregateRoot: IBoundedContext
{
    TAggregateRoot HandleLogic(TAggregateRoot aggregate);
}
