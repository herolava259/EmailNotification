using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class CreateOrderFaultEventConsumer : ConsumerBase<Fault<CreateOrderEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<CreateOrderEvent>> context)
    {
        Console.WriteLine("Order Create Faulted.");

        return Task.CompletedTask;
    }
}
