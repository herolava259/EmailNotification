using Confluent.Kafka;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static MongoDB.Driver.WriteConcern;

namespace Playground.Application.Example.Kafka.DI.Extensions;


public static class KafkaConfigurationAbstraction
{
    public static IServiceCollection ConfigKafkaConsumer<TKey, TValue>(this IServiceCollection serviceCollection, Func<ConsumerConfig> configFactory)
        => serviceCollection.AddSingleton(new ConsumerBuilder<TKey, TValue>(configFactory()).Build());

    public static IServiceCollection ConfigKafkaConsumer<TKey, TValue>(this IServiceCollection serviceCollection, Func<ConsumerBuilder<TKey, TValue>> builderFactory)
        => serviceCollection.AddSingleton(builderFactory().Build());

    public static IServiceCollection ConfigKafkaConsumer<TKey, TValue>(this IServiceCollection serviceCollection, IConfiguration _configuration,
                                                        string groupId, AutoOffsetReset autoOffsetReset = AutoOffsetReset.Earliest)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _configuration["ConnectionString:Kafka:BootstrapServers"],
            GroupId = groupId,
            AutoOffsetReset = autoOffsetReset
        };

        return serviceCollection.AddSingleton(new ConsumerBuilder<TKey, TValue>(consumerConfig).Build());
    }

    public static IServiceCollection ConfigKafkaProducer<TKey, TValue>(this IServiceCollection serviceCollection, Func<ProducerConfig> configFactory)
        => serviceCollection.AddSingleton(new ProducerBuilder<TKey, TValue>(configFactory()).Build());

    public static IServiceCollection ConfigKafkaProducer<TKey, TValue>(this IServiceCollection serviceCollection, Func<ProducerBuilder<TKey, TValue>> builderFactory)
        => serviceCollection.AddSingleton(builderFactory().Build());

    public static IServiceCollection ConfigKafkaProducer<TKey, TValue>(this IServiceCollection serviceCollection, IConfiguration _configuration,
                                                        string clientId)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _configuration["ConnectionString:Kafka:BootstrapServers"],
            ClientId = clientId
        };

        return serviceCollection.AddSingleton(new ProducerBuilder<TKey, TValue>(producerConfig).Build());
    }
}

public static class KafkaConfigurationDetail
{
    #region config channel for order service

    // consumer config 

    // producer config

    #endregion

    #region config channel for notfication service

    // send email consumer config 

    #endregion 

}
