using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Core;

public sealed record WebhookSubscription(Guid Id, string EventType, string WebhookUrl, 
                                       DateTimeOffset CreatedOnUtc, DateTimeOffset? ExpiredAtUtc = null)
{

}
