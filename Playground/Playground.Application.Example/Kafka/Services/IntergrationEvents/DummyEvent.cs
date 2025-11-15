using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.IntergrationEvents;

public sealed record DummyEvent(string CorrelationId, DateTimeOffset CreationDate, string Name = "Groot", string Action = "I'am Groot")
    :BaseIntergrationEvent(CorrelationId, CreationDate)
{
    public string Speak()
        => Action;
}
