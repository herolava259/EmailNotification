using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger;

public interface IAmbientContext
{
    void Set<T>(string key, [MaybeNull] T value);

    [return: MaybeNull]
    T Get<T>(string key);
}

public interface ITriggerContext: IAmbientContext
{
    
    void Initialize(TriggerSourceType triggerSourceType,
                    TriggerType triggerType,
                    string? agentId,
                    string? correlationId = null);

    string? CorrelationId { get; }

    TriggerSourceType SourceType { get; }

    string AgentId { get; }

    TriggerType TriggerType { get; }

    TriggerState State { get; }
}


public sealed class TriggerContext : ITriggerContext
{
    public static readonly AsyncLocal<TriggerState> _state = new();

    private static readonly ConcurrentDictionary<string, AsyncLocal<object?>>
        _contexts = new(StringComparer.Ordinal);

    public string? CorrelationId
    {
        get
        {
            if (_state.Value is null)
                return String.Empty;

            return _state.Value.CorrelationId;
        }
    }

    public TriggerSourceType SourceType => throw new NotImplementedException();

    public string AgentId
    {
        get
        {
            if (_state.Value is null)
                return String.Empty;

            return _state.Value.AgentId;
        }
    }

    public TriggerType TriggerType
    {
        get
        {
            if (_state.Value is null)
                return TriggerType.None;

            return _state.Value.Type;
        }
    }

    public TriggerState State
    {
        get
        {
            if (_state.Value is null)
                return TriggerState.Default;

            return _state.Value;
        }
    }

    [return: MaybeNull]
    public T Get<T>(string key)
    {
        return _contexts.TryGetValue(key, out AsyncLocal<object?>? keyCtx)
                ? (T) (keyCtx!.Value ?? default(T)!)
                : default(T);
    }

    public void Initialize(TriggerSourceType triggerSourceType, TriggerType triggerType, string? agentId, string? correlationId = null)
    {
        _state.Value = new TriggerState
        {
            CorrelationId = correlationId ?? Guid.NewGuid().ToString(),
            SourceType = triggerSourceType,
            Type = triggerType,
            AgentId = agentId ?? String.Empty,
        };
    }

    public void Set<T>(string key, [MaybeNull] T value)
    {
        AsyncLocal<object?> keyCtx = _contexts.AddOrUpdate(key, 
                                                            k => new AsyncLocal<object?>(),
                                                            (k, al) => al);
        keyCtx.Value = (object?)value;
    }
}
