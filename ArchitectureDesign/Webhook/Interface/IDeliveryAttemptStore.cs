using System.Net;
using Webhook.Core;

namespace Webhook.Interface;

public interface IDeliveryAttemptStore
{
    public ValueTask<bool> AddFirstAttempt(WebhookDeliveryAttempt attempt);

    public ValueTask<bool> PersistAttemptFailure(Guid attemptId, 
                                                 HttpStatusCode statusCode);

    public ValueTask<bool> PersistAttemptSuccess(Guid attemptId,
                                                 HttpStatusCode statusCode);

    public ValueTask<IReadOnlyCollection<WebhookDeliveryAttempt>> RetrieveDeliveryAttemptFailures(int windowSize = 10, int windowNo = 0);

    public ValueTask<int> TotalOfDeliveryAttemptFailure();


}
