using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class TakePaymentFaultEventConsumer : ConsumerBase<Fault<TakePaymentEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<TakePaymentEvent>> context)
    {
        Console.WriteLine("Payment Taken Faulted");

        return Task.CompletedTask;

    }
}
