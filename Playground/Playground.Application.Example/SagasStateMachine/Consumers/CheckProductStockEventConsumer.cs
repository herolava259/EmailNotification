using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class CheckProductStockEventConsumer : ConsumerBase<CheckProductStockEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<CheckProductStockEvent> context)
    {
        Console.WriteLine("Product Stock checked.");


        context.RespondAsync(new CheckProductStockEventDto
        {
            OrderId = context.Message.OrderId
        });

        return Task.CompletedTask;
    }
}