using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.IntergrationEvents;

public abstract record CounterIntergrationEvent(string CorrelationId, DateTimeOffset CreationDate, ulong Key = 1): BaseIntergrationEvent(CorrelationId, CreationDate)
{ }

public abstract class AggregatorEvent<TEvent>
    where TEvent : CounterIntergrationEvent
{
    protected AggregatorEvent(ulong key)
    {
        _key = key;
    }
    public ICollection<TEvent> Events { get; private init; } = new List<TEvent>();

    private readonly ulong _key = 0;

    public ulong Key { get { return _key; } }

    public abstract bool Finish { get; }

    public virtual bool Accept(TEvent @event)
    {
        if (@event.Key != _key)
            return false;
        Events.Add(@event);

        return true;
    }
}
