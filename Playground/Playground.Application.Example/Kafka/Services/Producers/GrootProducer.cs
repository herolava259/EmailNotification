using Playground.Application.Example.Kafka.Core;
using Playground.Application.Example.Kafka.Services.Bases;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Producers;

public sealed class Aggreroot: EntityBase, IAggregateRoot
{
    public int Height { get; set; }

    public int Weight { get; set; }
}

public sealed record GrootTalkEvent(Guid AggregateId,DateTimeOffset OccuredTime, string Action = "I'm Grrrrrot") 
    : DomainEventBase<Aggreroot>(AggregateId, OccuredTime)
{
}

public sealed class GrootProducer : BaseKafkaProducer<GrootTalkEvent>
{
    public GrootProducer(IServiceProvider serviceProvider, string defaultTopicName = "default-topic") : base(serviceProvider, defaultTopicName)
    {
    }

    public override async Task ProceedAsync(GrootTalkEvent @event, CancellationToken ct = default)
    {
        await base.ProduceAsync(new DummyEvent(Guid.NewGuid().ToString(), 
            DateTimeOffset.UtcNow, Name: typeof(Aggreroot).Name, Action: @event.Action), ct);
    }
}
