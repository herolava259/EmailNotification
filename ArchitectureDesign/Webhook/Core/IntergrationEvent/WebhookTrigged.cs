using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Core.IntergrationEvent;

public sealed record WebhookTriggered(Guid SubscriptionId,
                                       string EventType,
                                       string WebhookUrl,
                                       object Data)
{

}
