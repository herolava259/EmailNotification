using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Abstractions.Advances;

public interface IEventProxy<TEvent>
    where TEvent: BaseIntergrationEvent
{
    Task<bool> ForwardAsync(RoutingEventDecorator<TEvent> eventWrapper);
}

internal class EventProxy<TEvent> : IEventProxy<TEvent>
    where TEvent : BaseIntergrationEvent
{
    public Task<bool> ForwardAsync(RoutingEventDecorator<TEvent> eventWrapper)
    {
        // TODO: implement later 
        throw new NotImplementedException();
    }
}
