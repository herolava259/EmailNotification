using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.IntergrationEvents;

public abstract record BaseIntergrationEvent(string CorrelationId, DateTimeOffset CreationDate)
{
}
