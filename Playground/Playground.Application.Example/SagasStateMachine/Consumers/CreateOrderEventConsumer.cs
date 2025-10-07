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

public class CreateOrderEventConsumer : ConsumerBase<CreateOrderEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<CreateOrderEvent> context)
    {
        Console.WriteLine("Order Created");

        context.RespondAsync(new CreateOrderEventDto
        {
            OrderId = context.Message.OrderId
        });

        //to see what's going on when we got an error.
        throw new Exception("Transition Fault State");

        return Task.CompletedTask;
    }
}
