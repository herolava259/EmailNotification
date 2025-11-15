
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Example.Kafka.Services.Bases;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using Microsoft.Extensions.Logging;

namespace Playground.Application.Example.Kafka.Services.Consumers;


internal sealed class DummyConsumer : BaseKafkaConsumer<DummyEvent>
{
    private readonly ILogger<DummyConsumer> _logger;
    public DummyConsumer(IServiceProvider serviceProvider, string topicName = "default-topic") : base(serviceProvider, topicName)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        _logger = loggerFactory.CreateLogger<DummyConsumer>();
    }

    protected override async Task ConsumeAsync(DummyEvent @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Don't talk: '{@event.Speak()}'");
        Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

    }
}
