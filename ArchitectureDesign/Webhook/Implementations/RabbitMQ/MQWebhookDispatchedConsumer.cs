using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webhook.Core.IntergrationEvent;
using Webhook.Interface;

namespace Webhook.Implementations.RabbitMQ;

public sealed class MQWebhookDispatchedConsumer(ISubscriptionStore _store) : IConsumer<WebhookDispatched>
{
    public async Task Consume(ConsumeContext<WebhookDispatched> context)
    {
        var message = context.Message;

        var subscriptions = await _store.GetSubscriptions(message.EventType);

        foreach(var sc in subscriptions)
        {
            await context.Publish(new WebhookTriggered(
                sc.Id,
                sc.EventType,
                sc.WebhookUrl,
                message.Data)
            );
        }

        //await context.PublishBatch(subscriptions.Select(sc => new WebhookTriggered(
        //        sc.Id,
        //        sc.EventType,
        //        sc.WebhookUrl,
        //        message.Data)));
    }
}
