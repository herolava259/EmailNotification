using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Example.SagasStateMachine.Settings;
using System.Text.Json;
namespace Playground.Application.Example.SagasStateMachine.DI.Extensions;

public static class SagasSetupExtensions
{
    public static IServiceCollection AddSagasOrderProcessing(this IServiceCollection serviceRegistration, IConfiguration configuration)
    {
        var messageBrokerQueueSettings = JsonSerializer.Deserialize<MessageBrokerQueueSettings>
            (configuration.GetSection("MessageBroker:QueueSettings").Value!);
        var messageBrokerPersistenceSettings = JsonSerializer.Deserialize<MessageBrokerPersistenceSettings>
            (configuration.GetSection("MessageBroker:StateMachinePersistence").Value!);
        serviceRegistration.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<OrderStateMachine, OrderState>().MongoDbRepository(r =>
            {
                r.Connection = messageBrokerPersistenceSettings!.Connection;
                r.DatabaseName = messageBrokerPersistenceSettings!.DatabaseName;
                r.CollectionName = messageBrokerPersistenceSettings!.CollectionName;
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(messageBrokerQueueSettings!.HostName, messageBrokerQueueSettings.VirtualHost, h =>
                {
                    h.Username(messageBrokerQueueSettings.UserName);
                    h.Password(messageBrokerQueueSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        return serviceRegistration;
    }
}
