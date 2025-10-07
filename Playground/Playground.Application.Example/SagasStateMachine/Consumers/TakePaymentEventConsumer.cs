using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Dtos;
using Playground.Application.Example.SagasStateMachine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class TakePaymentEventConsumer : ConsumerBase<TakePaymentEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<TakePaymentEvent> context)
    {
        Console.WriteLine("Payment Taken");

        context.RespondAsync(new TakePaymentEventDto()
        {
            OrderId = context.Message.OrderId
        });

        return Task.CompletedTask;
    }
}
