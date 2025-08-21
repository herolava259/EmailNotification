using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public interface IUnitOfWork<in TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task SaveChangeAsync();

    Task SaveChangeAsync(TAggregateRoot aggregateRoot);

    void SaveChange();

    void SaveChange(TAggregateRoot aggregateRoot);

}
