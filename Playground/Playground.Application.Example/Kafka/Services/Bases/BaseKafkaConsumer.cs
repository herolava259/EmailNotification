using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Bases;

public abstract class BaseKafkaConsumer<TInCommingEvent>
    where TInCommingEvent: BaseIntergrationEvent
{
    protected readonly IConsumer<Ignore, string> _kafkaConsumer;

    private readonly ILogger<BaseKafkaConsumer<TInCommingEvent>> _logger;

    public string _topic { get; set; }
    protected BaseKafkaConsumer(IServiceProvider serviceProvider, string topicName = KafkaTopicConstants.Default)
    {
        _kafkaConsumer = serviceProvider.GetRequiredService<IConsumer<Ignore, string>>();

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        _logger = loggerFactory.CreateLogger<BaseKafkaConsumer<TInCommingEvent>>();

        _topic = topicName;

    }

    // TODO: declare abstract function pre-start, post-end later



    protected virtual async Task ExecuteAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Begin Execute async for {topic}", _topic);
        // log that cosumer subscribe which topic. write later 
        _kafkaConsumer.Subscribe(_topic);
        while(!ct.IsCancellationRequested)
        {
            // need to log
            var consumeResult = _kafkaConsumer.Consume(ct);

            _logger.LogInformation(
                @$"Topic: {consumeResult.Topic} - Partition: {consumeResult.Partition.Value} - Offset: {consumeResult.Offset.Value} - TimeStamp: {consumeResult.Timestamp.UtcDateTime}");

            var incomingMessage = consumeResult.Message;

            var message = incomingMessage.Value;
            var @event = JsonSerializer.Deserialize<TInCommingEvent>(message);

            if (@event is null)
                // log in here
                continue;

            await ConsumeAsync(@event, ct);
        }
    }

    protected abstract Task ConsumeAsync(TInCommingEvent @event, CancellationToken cancellationToken = default);
    
}
