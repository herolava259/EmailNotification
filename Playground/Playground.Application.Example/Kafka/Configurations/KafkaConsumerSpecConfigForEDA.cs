using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Configurations;

public sealed record KafkaConsumerSpecConfigForEDA(
    string Server,
    string ConsumerGroupId
    )
{
    // TODO: study and read configuration for eda later 
    // ensure idempotence consume, no dirty read, ensure a event's consumed exactly once
}
