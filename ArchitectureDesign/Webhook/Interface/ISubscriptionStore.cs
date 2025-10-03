using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webhook.Core;

namespace Webhook.Interface;

public interface ISubscriptionStore
{
    public Task Add(WebhookSubscription subscription);

    public Task Remove(Guid subscriptionId);

    public Task<IReadOnlyCollection<WebhookSubscription>> GetSubscriptions(string eventType);
}
