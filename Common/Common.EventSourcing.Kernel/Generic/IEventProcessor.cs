using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Generic;

public interface IEventAsyncProcessor
{
    Task Handle<TEvent>(TEvent @event);

    Task RewindBetween<TEvent>(TEvent @beginEvent, TEvent @endEvent);

    Task RewindTo<TEvent>(TEvent @event);


}
