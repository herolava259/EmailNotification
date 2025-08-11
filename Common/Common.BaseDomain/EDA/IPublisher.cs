using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.EDA;

public interface IPublisher<in TIntergrationEvent>
    where TIntergrationEvent: BaseIntergrationEvent
{
    Task PublishAsync(TIntergrationEvent @event);
}


