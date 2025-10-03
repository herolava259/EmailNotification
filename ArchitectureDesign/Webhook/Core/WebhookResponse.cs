
namespace Webhook.Core;

public sealed record WebhookResponse(Guid Id, string eventType, DateTimeOffset TimeStamp, object Payload)
{
    
}
