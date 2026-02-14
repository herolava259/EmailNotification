using Confluent.Kafka;


namespace Playground.Application.Example.Kafka.Configurations;


public enum IdempotenceStyle: ushort
{
    AtMostOnce = 0,
    AtLeastOnce = 1,
    ExactlyOnce = 2,
}


// configuration for application operation following eda style
public sealed record KafkaProducerSpecConfigForEDA(
    string Server,
    string ClientId,
    Partitioner RoutingType,
    int BatchNumMessages,
    CompressionType CompressionType,
    IdempotenceStyle IdempotenceStyle,
    int MessageQueueSize,
    string TransactionalName = "",
    bool EnsureOrder = true
    // addtional arguments for config
    )
{
    //TODO: thinking and adjust properties
    public int MessageSendMaxRetires
        => IdempotenceStyle switch
        {
            IdempotenceStyle.ExactlyOnce => Int32.MaxValue,
            IdempotenceStyle.AtMostOnce => 0,
            IdempotenceStyle.AtLeastOnce => Int32.MaxValue,
            _ => throw new NotImplementedException()
        };

    public bool EnableIdempotence
        => IdempotenceStyle switch
        {
            IdempotenceStyle.ExactlyOnce => true,
            IdempotenceStyle.AtMostOnce => true,
            IdempotenceStyle.AtLeastOnce => false,
            _ => throw new NotImplementedException()
        };

    public string TransactionalId
        => IdempotenceStyle switch
        {
            IdempotenceStyle.ExactlyOnce => TransactionalName,
            IdempotenceStyle.AtMostOnce => string.Empty,
            IdempotenceStyle.AtLeastOnce => string.Empty,
            _ => throw new NotImplementedException()
        };


}
