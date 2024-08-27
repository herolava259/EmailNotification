using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NotificationSystem.Behaviours;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NotificationSystem.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInMemoryEvent<T, THandler>(this IServiceCollection services)
        where THandler : class, IEventHandler<T>
    {
        var bus = Channel.CreateUnbounded<Event<T>>(
            new UnboundedChannelOptions
            {
                AllowSynchronousContinuations = false
            }
        );

        services.AddScoped<IEventHandler<T>, THandler>();

        services.AddSingleton(typeof(IProducer<T>), _ => new InMemoryEventBusProducer<T>(bus.Writer));
        services.AddSingleton<CancellationTokenSource>();
        var consumerFactory = (IServiceProvider provider) => new InMemoryEventBusConsumer<T>(
                bus.Reader,
                provider.GetRequiredService<IServiceScopeFactory>(),
                provider.GetRequiredService<ILoggerFactory>().CreateLogger<InMemoryEventBusConsumer<T>>()
            );

        services.AddSingleton(typeof(IConsumer), consumerFactory.Invoke);
        services.AddSingleton(typeof(IConsumer<T>), consumerFactory.Invoke);
        services.TryAddSingleton(typeof(IEventContextAccessor<>), typeof(EventContextAccessor<>));

        return services;
    }

    public static async Task<IServiceProvider> StartConsumers(this IServiceProvider serviceProvider)
    {
        var consumers = serviceProvider.GetServices<IConsumer>();

        foreach (var consumer in consumers)
        {
            await consumer.Start().ConfigureAwait(false);
        }

        return serviceProvider;
    }

    public static async Task<IServiceProvider> StartConsumers(this IServiceProvider services, CancellationToken parentToken)
    {
        var consumers = services.GetServices<IConsumer>();

        foreach (var consumer in consumers)
        {
            await consumer.Start(parentToken).ConfigureAwait(false);
        }

        return services;
    }

    public static async Task<IServiceProvider> StopConsumers(this IServiceProvider services)
    {
        var consumers = services.GetServices<IConsumer>();
        foreach (var consumer in consumers)
        {
            await consumer.Stop().ConfigureAwait(false);
        }

        return services;
    }
}