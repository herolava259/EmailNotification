using Common.Domain.Generic.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BaseDomain.DDD;

public interface IAgreegateRoot
{

    public bool EnsureACID();

    public bool RaiseEvent<TDomainEvent>(BaseDomainEvent @event)
        where TDomainEvent: BaseDomainEvent;
}
