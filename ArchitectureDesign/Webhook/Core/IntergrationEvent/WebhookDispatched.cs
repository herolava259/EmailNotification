using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Core.IntergrationEvent;

public sealed record WebhookDispatched(string EventType, object Data)
{
}
