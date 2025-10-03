using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using Webhook.Core;
using Webhook.Interface;
using Webhook.Constants;
using System.Diagnostics;

namespace Webhook.Implementation;

public sealed class WebhookProcessor : IWebhookDispatcher
{
    private readonly HttpClient _httpClient;
    private readonly ISubscriptionStore _subscriptionStore;
    private readonly ChannelWriter<WebhookEvent> _writer;

    public WebhookProcessor(HttpClient _httpClient, ISubscriptionStore _subscriptionStore, ChannelWriter<WebhookEvent> writer)
    {
        this._httpClient = _httpClient;
        this._subscriptionStore = _subscriptionStore;
        this._writer = writer;
    }
    public async Task DispatchAsync(string eventType, object payload)
    {
        using var activity = DiagnosticConfiguration.Source.StartActivity($"{eventType} dispatch webhook");

        activity?.AddTag("event.type", eventType);

        await _writer.WriteAsync(new WebhookEvent(eventType, payload, activity?.Id));
    }

    public async Task ProceedAsync<TPayload>(string eventType, TPayload payload)
    {
        var subscriptions = await _subscriptionStore.GetSubscriptions(eventType);

        foreach (var subscription in subscriptions)
        {
            var request = new WebhookResponse(Guid.NewGuid(), subscription.EventType, DateTimeOffset.UtcNow, payload);

            await _httpClient.PostAsJsonAsync(subscription.WebhookUrl, request);
        }
    }
}
