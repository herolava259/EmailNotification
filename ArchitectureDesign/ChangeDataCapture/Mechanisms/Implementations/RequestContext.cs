using ChangeDataCapture.Mechanisms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Mechanisms.Implementations;




public sealed class RequestContext : IRequestContext
{

    private sealed class ContextState
    {
        public string? CorrelationId { get; set; } = default!;

        public ExecutionSource SourceType { get; set; } = ExecutionSource.Unknown;

        public string? SourceId { get; set; } = string.Empty;

        public string? UserId { get; set; } = string.Empty;
    }


    private static readonly AsyncLocal<ContextState> _state = new();

    public void Initialize(ExecutionSource sourceType,
                           string? userId = "",
                           string? sourceId = "",
                           string? correlationId = null)
    {
        _state.Value = new ContextState
        {
            CorrelationId = correlationId ?? Guid.NewGuid().ToString(),
            SourceType = sourceType,
            SourceId = sourceId ?? string.Empty,
            UserId =  sourceId ?? string.Empty
        };
    }

    public string CorrelationId
    {
        get
        {
            if (_state.Value is null)
                return string.Empty;

            _state.Value.CorrelationId ??= Guid.NewGuid().ToString();

            return _state.Value.CorrelationId;
        }
    }

    public ExecutionSource SourceType { 
        get
        {
            if (_state.Value is null)
                return ExecutionSource.Unknown;
            return _state.Value.SourceType;
        }
    }

    public string? UserId
    {
        get
        {
            if (_state.Value is null)
                return string.Empty;
            return _state.Value.UserId;
        }
    }

    public string? SourceId
    {
        get
        {
            if (_state.Value is null)
                return string.Empty;

            return _state.Value.SourceId;
        }
    }
}

// Dependency Injection 

