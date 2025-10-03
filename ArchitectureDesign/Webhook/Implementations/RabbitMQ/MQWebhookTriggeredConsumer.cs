using MassTransit;
using System.Net.Http.Json;
using System.Text.Json;
using Webhook.Core;
using Webhook.Core.IntergrationEvent;
using Webhook.Interface;

namespace Webhook.Implementations.RabbitMQ;

public sealed class MQWebhookTriggeredConsumer(IHttpClientFactory _httpClientFactory,
                                               IDeliveryAttemptStore _attemptStore) : IConsumer<WebhookTriggered>
{
    public async Task Consume(ConsumeContext<WebhookTriggered> context)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var payload = new WebhookPayload
        {
            Id = Guid.NewGuid(),
            EventType = context.Message.EventType,
            SubscriptionId = context.Message.SubscriptionId,
            TimeStamp = DateTimeOffset.UtcNow,
            Data = context.Message.Data,
        };

        var jsonPayload = JsonSerializer.Serialize(payload);

        try
        {
            var response = await httpClient.PostAsJsonAsync(context.Message.WebhookUrl, payload);

            response.EnsureSuccessStatusCode();

            var attempt = WebhookDeliveryAttempt.FirstAttempt(response, context.Message, jsonPayload);

            _ = await _attemptStore.AddFirstAttempt(attempt);
        }
        catch (Exception ex) {
            await _attemptStore.AddFirstAttempt(WebhookDeliveryAttempt.ExceptionAtFirst(context.Message, jsonPayload));
        }
    }
}
