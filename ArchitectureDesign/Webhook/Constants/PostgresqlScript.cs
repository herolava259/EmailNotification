using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Constants;

internal static class PostgresqlScript
{
    public const string SubscriptionTableCreationScript = $@"
DROP TABLE IF EXISTS WebhookSubscription
CREATE TABLE WebhookSubscription(Id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
                                 EventType VARCHAR(255) NOT NULL
                                 WebhookUrl VARCHAR(255) NOT NULL
                                 CreatedOnUtc TIMESTAMPTZ NOT NULL
                                 ExpiredAt TIMESTAMPTZ);

CREATE INDEX idx_webhook_subscription_event_type ON WebhookSubscription USING HASH(EventType);
";

}
