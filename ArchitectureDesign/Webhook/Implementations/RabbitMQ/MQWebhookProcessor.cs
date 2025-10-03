using MassTransit;
using System.Net.Http.Json;
using Webhook.Core;
using Webhook.Core.IntergrationEvent;
using Webhook.Interface;

namespace Webhook.Implementations.RabbitMQ;

public class MQWebhookProcessor(HttpClient _httpClient, 
                                 ISubscriptionStore _subscriptionStore,
                                 IPublishEndpoint _publishEndpoint) : IWebhookDispatcher
{
    public async Task DispatchAsync(string eventType, object payload)
    {
        await _publishEndpoint.Publish(new WebhookDispatched(eventType, payload));
    }

    public async Task ProceedAsync<TPayload>(string eventType, TPayload payload)
        where TPayload: notnull
    {
        var subscriptions = await _subscriptionStore.GetSubscriptions(eventType);



        //foreach (var subscription in subscriptions)
        //{
        //    var request = new WebhookResponse(Guid.NewGuid(), subscription.EventType, DateTimeOffset.UtcNow, payload);

        //    await _httpClient.PostAsJsonAsync(subscription.WebhookUrl, request);
        //}

        await Task.WhenAll(subscriptions.Select(sc => _httpClient.PostAsJsonAsync(sc.WebhookUrl,
                                                      new WebhookResponse(Guid.NewGuid(), 
                                                      sc.EventType, 
                                                      DateTimeOffset.UtcNow, payload))));
    }
}
