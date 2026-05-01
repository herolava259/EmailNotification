using MassTransit;
using Microsoft.Extensions.Configuration;

namespace ChangeDataCapture.SourceTrigger.Initiators;

public sealed class AttachTriggerContextSendObeserver(ITriggerContext _triggerContext,
                                            IConfiguration _configuration) : ISendObserver
{
    public Task PostSend<T>(SendContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PreSend<T>(SendContext<T> context) where T : class
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

    public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }
}
