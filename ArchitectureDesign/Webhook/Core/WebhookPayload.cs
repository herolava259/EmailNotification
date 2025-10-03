using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Core;

public class WebhookPayload
{
    public Guid Id { get; set; }

    public string EventType { get; set; } = String.Empty;

    public Guid SubscriptionId { get; set; }

    public DateTimeOffset TimeStamp { get; set; }

    public object Data { get; set; }

}
