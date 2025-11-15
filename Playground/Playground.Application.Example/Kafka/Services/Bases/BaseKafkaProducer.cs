using Confluent.Kafka;
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

public abstract class BaseKafkaProducer<TDomainEvent>
{
    protected readonly IProducer<Null, string> _kafkaProducer;

    private readonly ILogger<BaseKafkaProducer<TDomainEvent>> _logger;

    public string _defaultTopic { get; set; }
    protected BaseKafkaProducer(IServiceProvider serviceProvider, string defaultTopicName = KafkaTopicConstants.Default)
    {
        _kafkaProducer = serviceProvider.GetRequiredService<IProducer<Null, string>>();

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        _logger = loggerFactory.CreateLogger<BaseKafkaProducer<TDomainEvent>>();

        _defaultTopic = defaultTopicName;

    }

    public abstract Task ProceedAsync(TDomainEvent @event, CancellationToken ct = default);

    protected virtual async Task ProduceAsync<TEmmitEvent>(TEmmitEvent @event, CancellationToken cancellationToken = default)
        where TEmmitEvent: BaseIntergrationEvent
    {
        await ProduceAsync(@event,topic: _defaultTopic, cancellationToken);
    }

    protected virtual async Task ProduceAsync<TEmmitEvent>(TEmmitEvent @event,string topic, CancellationToken cancellationToken = default)
        where TEmmitEvent: BaseIntergrationEvent
    {
        var kafkaMessage = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(@event)
        };

        var deliveryResult = await _kafkaProducer.ProduceAsync(topic: topic, kafkaMessage, cancellationToken);

        _logger.LogInformation
            ($"Event's send to Topic: {deliveryResult.Topic} - Partition: {deliveryResult.Partition.Value} - Offset: {deliveryResult.Offset.Value} at {deliveryResult.Timestamp.UtcDateTime}.");
    }
}
