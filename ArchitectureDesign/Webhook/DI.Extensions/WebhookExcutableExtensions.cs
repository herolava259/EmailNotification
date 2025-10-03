using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Webhook.Implementation;
using Webhook.Implementations.RabbitMQ;
using Webhook.Interface;

namespace Webhook.DI.Extensions;

public static class WebhookExcutableExtensions
{

    public static IServiceCollection AddAsyncPubSubWebhookEvent(this IServiceCollection serviceCollection)
    {
        var bus = Channel.CreateUnbounded<WebhookEvent>(
            new UnboundedChannelOptions
            {
                AllowSynchronousContinuations = false
            });



        serviceCollection.AddSingleton(typeof(WebhookProcessor), serviceProvider => new WebhookProcessor(
            serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("Webhook-Http-Client"),
            serviceProvider.GetRequiredService<ISubscriptionStore>(),
            bus.Writer
            ));

        serviceCollection.AddHostedService<WebhookBroadcastWorker>(
            serviceProvider => new WebhookBroadcastWorker(serviceProvider.GetRequiredService<IServiceScopeFactory>(),
                                                          bus.Reader));

        return serviceCollection;
    }

    public static IServiceCollection AddMassTransitForPublishingWebhook(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            busConfig.AddConsumer<MQWebhookDispatchedConsumer>();
            busConfig.AddConsumer<MQWebhookTriggeredConsumer>();

            busConfig.UsingRabbitMq((ctx, config) =>
            {
                config.Host(configuration.GetConnectionString("rabbitmq"));
                config.ConfigureEndpoints(ctx);
            });
        });
}
