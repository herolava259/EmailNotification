using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger.Initiators;

internal class AttachTriggerContextPublishObserver(ITriggerContext _triggerContext,
                                            IConfiguration _configuration) : IPublishObserver
{
    public Task PostPublish<T>(PublishContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PrePublish<T>(PublishContext<T> context) where T : class
    {
        var corrId = _triggerContext.CorrelationId 
                    ?? context.CorrelationId?.ToString()
                    ?? Guid.NewGuid().ToString();

        var serviceId = _triggerContext.AgentId
                            ?? _configuration["ServiceId"]
                            ?? string.Empty;

        context.Headers.Set(TriggerConstantKeys.CorrelationIdKey, corrId);
        context.Headers.Set(TriggerConstantKeys.ServiceIdKey, serviceId);

        return Task.CompletedTask;
    }

    public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }
}
