using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playground.Application.Example.Kafka.Interfaces;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;

namespace Playground.Application.Example.Kafka.Services.Bases;

public abstract class BaseFanoutConsumer<TEvent> : BaseKafkaConsumer<TEvent>, ISubscriable<TEvent>
    where TEvent : BaseIntergrationEvent
{
    private readonly ILogger<BaseFanoutConsumer<TEvent>> _logger;

    protected BaseFanoutConsumer(
                    ILogger<BaseFanoutConsumer<TEvent>> logger,
                    IServiceProvider serviceProvider, 
                    string topicName = "default-topic") : base(serviceProvider, topicName)
    {
        _logger = logger;
    }


    public event Func<TEvent, CancellationToken, Task> OnEventOccurred = static (_, _) => Task.CompletedTask;



    protected override async Task ConsumeAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        //logging and some validation check operations
        await OnEventOccurred(@event, cancellationToken);
    }
}
