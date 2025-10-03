using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Implementation;

public sealed record WebhookEvent(string EventType, object Payload, string? ParentActivityId)
{
}
