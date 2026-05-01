using MassTransit;
using Microsoft.Extensions.Configuration;

namespace ChangeDataCapture.SourceTrigger.Initiators;

public sealed class AttachTriggerContextConsumeObserver(ITriggerContext _triggerContext,
                                            IConfiguration _configuration
                                            ) : IConsumeObserver
{
    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        var corrId = _triggerContext.CorrelationId
                    ?? context.CorrelationId?.ToString()
                    ?? Guid.NewGuid().ToString();

        var agentId = context.Headers.Get<string>(TriggerConstantKeys.ProducerIdKey)
                        ?? _configuration[TriggerConstantKeys.ProducerIdKey]
                        ?? string.Empty;

        var sourceType = context.Headers.Get<string>(TriggerConstantKeys.SourceTypeKey)
                            ?? TriggerSourceType.None.ToString();

        _triggerContext.Initialize(Enum.Parse<TriggerSourceType>(sourceType),
                                   TriggerType.MesageBroker,
                                   agentId);
        
        return Task.CompletedTask;
    }
}
