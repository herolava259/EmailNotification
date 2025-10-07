using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using Playground.Application.Example.SagasStateMachine.Dtos;
using Playground.Application.Example.SagasStateMachine.Events;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class OrderProcessInitializationEventConsumer : ConsumerBase<OrderProcessInitializationEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<OrderProcessInitializationEvent> context)
    {
        Console.WriteLine("Order process Initialized");

        context.RespondAsync(new OrderProcessInitiazationDto
        {
            OrderId = context.Message.OrderId
        });

        return Task.CompletedTask;
    }
}