using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Interfaces;

public interface ISubscriable
{
}

public interface ISubscriable<TEvent>: ISubscriable
    where TEvent: BaseIntergrationEvent
{
    event Func<TEvent, CancellationToken, Task> OnEventOccurred;

}
