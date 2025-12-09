using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Abstractions.Advances;

public static class RoutingHelper
{
    public static bool Match<TEvent>(this RoutingEventDecorator<TEvent> wrapper, string endpointKey)
        where TEvent : BaseIntergrationEvent
    {
        // declare rule for routing like patter match with endpointKey
        throw new NotImplementedException();
    }
}
