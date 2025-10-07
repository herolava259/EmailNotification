using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Webhook.Core.IntergrationEvent;

namespace Webhook.Core;

public sealed record WebhookDeliveryAttempt(Guid Id, 
                            Guid SubscriptionId,
                            Guid AttemptId,
                            string Payload, 
                            int? ResponseStatusCode,
                            bool Success,
                            DateTimeOffset TimeStamp,
                            int NumOfAttemp = 0)
{
    public WebhookDeliveryAttempt Recreate(HttpStatusCode? statusCode, bool isSuccess)
        => new(Guid.NewGuid(), this.SubscriptionId,this.AttemptId, this.Payload, 
            statusCode is null? null : (int)statusCode, 
            isSuccess, DateTimeOffset.UtcNow, this.NumOfAttemp + 1);

    public static WebhookDeliveryAttempt First(HttpStatusCode? statusCode, bool isSuccess,
                                        Guid subscriptionId,
                                        string payload)
        => new(Guid.NewGuid(), subscriptionId, Guid.NewGuid(), payload, statusCode is null ? null : (int)statusCode,
                Success: isSuccess, DateTimeOffset.UtcNow);

    public static WebhookDeliveryAttempt FirstAttempt(HttpResponseMessage response,
                                                      WebhookTriggered trigger,
                                                      string? payload)
        => First(response?.StatusCode, response is not null && response.IsSuccessStatusCode,
                 trigger.SubscriptionId, payload ?? String.Empty);

    public static WebhookDeliveryAttempt ExceptionAtFirst(WebhookTriggered trigger, string payload)
        => First(null, false, trigger.SubscriptionId, payload);

    public WebhookDeliveryAttempt RecreateAfterFailure(HttpStatusCode statusCode)
        => Recreate(statusCode, isSuccess: false);

    public WebhookDeliveryAttempt FailureDueToException()
        => Recreate(statusCode: null, isSuccess: false);

    public WebhookDeliveryAttempt TerminateAfterSuccess(HttpStatusCode statusCode)
        => Recreate(statusCode, isSuccess: true);
}
