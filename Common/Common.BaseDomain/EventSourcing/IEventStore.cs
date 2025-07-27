
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.EventSourcing;

public interface IEventStore<TStreamEvent>
{
    Task<IEnumerable<TStreamEvent>> LoadAsync(string eventBranch);

    Task<IEnumerable<TStreamEvent>> LoadAsync();

    ValueTask<bool> SaveAsync(IEnumerable<TStreamEvent> events);

    ValueTask<bool> ExistAsync(string eventBranch);

    ValueTask<bool> ExistAsync(TStreamEvent @event);
}
