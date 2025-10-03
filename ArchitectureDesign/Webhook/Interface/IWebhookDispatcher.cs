using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Interface;

public interface IWebhookDispatcher
{

    Task DispatchAsync(string eventType, object payload);

    Task ProceedAsync<TPayload>(string eventType, TPayload payload)
        where TPayload: notnull;

}
