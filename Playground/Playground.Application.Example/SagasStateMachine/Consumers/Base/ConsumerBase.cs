using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Consumers.Base;

public abstract class ConsumerBase<T> : IConsumer<T> where T : class
{
    public async Task Consume(ConsumeContext<T> context)
    {
        try
        {
            await ConsumeInternal(context);
        }
        catch (Exception e)
        {
            await context.Publish<Fault<T>>(context);

            // global exception handling
            throw;
        }
    }

    protected abstract Task ConsumeInternal(ConsumeContext<T> context);
}
