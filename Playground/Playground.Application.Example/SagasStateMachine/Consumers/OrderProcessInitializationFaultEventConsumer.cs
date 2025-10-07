using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class OrderProcessInitializationFaultEventConsumer : ConsumerBase<Fault<OrderProcessInitializationEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<OrderProcessInitializationEvent>> context)
    {
        Console.WriteLine("Order Process Intialization faulted.");

        return Task.CompletedTask;
    }
}
