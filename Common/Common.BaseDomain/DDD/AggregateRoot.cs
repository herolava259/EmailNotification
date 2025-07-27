using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public abstract class AggregateRoot : DomainEntity, IOriginator<IAggregateRootMemento>
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
    public abstract IAggregateRootMemento ToSnapshot();
}

public interface IAggregateRootMemento : IMemento<AggregateRoot>
{
}
