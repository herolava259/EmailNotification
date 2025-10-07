using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class OrderProcessFailedEventConsumer : ConsumerBase<OrderProcessFailedEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<OrderProcessFailedEvent> context)
    {
        Console.WriteLine("Order Process Failed !");

        return Task.CompletedTask;
    }
}
