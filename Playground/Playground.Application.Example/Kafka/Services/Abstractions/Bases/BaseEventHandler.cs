using Playground.Application.Example.Kafka.Interfaces;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Abstractions.Bases;

public abstract class BaseEventHandler<TEvent> : Interfaces.IObserver<TEvent>
    where TEvent : BaseIntergrationEvent
{
    public void Subscribe(ISubscriable<TEvent> subject)
    {
        subject.OnEventOccurred += HandleAsync;
    }

    public abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    public abstract ValueTask DisposeAsync();
}
