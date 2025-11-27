using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Interfaces;

public interface IExecutionHandler<TEvent>
    where TEvent: BaseIntergrationEvent
{
    Task<TEvent> ExecuteAsync(TEvent @event, CancellationToken cancellationToken = default);
}
