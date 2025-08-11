using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.EDA;

public interface ISubscriber<in TIntergrationEvent>
    where TIntergrationEvent: BaseIntergrationEvent
{
    public Task OnNotifyAsync(TIntergrationEvent @event, CancellationToken cancellationToken = default);
}
