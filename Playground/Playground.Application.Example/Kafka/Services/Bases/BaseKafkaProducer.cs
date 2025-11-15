using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Bases;

public abstract class BaseKafkaProducer<TEmmitEvent>
{
    protected readonly IProducer<Null, string> _kafkaProducer;

    private readonly ILogger<BaseKafkaProducer<TEmmitEvent>> _logger;

    public string _defaultTopic { get; set; }
    protected BaseKafkaProducer(IServiceProvider serviceProvider, string defaultTopicName = KafkaTopicConstants.Default)
    {
        _kafkaProducer = serviceProvider.GetRequiredService<IProducer<Null, string>>();

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        _logger = loggerFactory.CreateLogger<BaseKafkaProducer<TEmmitEvent>>();

        _defaultTopic = defaultTopicName;

    }



    public virtual async Task ProduceAsync(TEmmitEvent @event, CancellationToken cancellationToken = default)
    {
        var kafkaMessage = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(@event)
        };

        var deliveryResult = await _kafkaProducer.ProduceAsync(topic: _defaultTopic, kafkaMessage, cancellationToken);

        _logger.LogInformation
            ($"Event's send to Topic: {deliveryResult.Topic} - Partition: {deliveryResult.Partition.Value} - Offset: {deliveryResult.Offset.Value} at {deliveryResult.Timestamp.UtcDateTime}.");
    }
}
