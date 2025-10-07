using MassTransit;
using Playground.Application.Example.SagasStateMachine.Consumers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers;

public class CheckProductStockFaultEventConsumer : ConsumerBase<Fault<CheckProductStockEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<CheckProductStockEvent>> context)
    {
        Console.WriteLine("Product Stock check Faulted.");

        return Task.CompletedTask;
    }
}
