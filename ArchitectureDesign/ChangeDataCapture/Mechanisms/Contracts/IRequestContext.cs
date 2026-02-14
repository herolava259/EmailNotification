using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Mechanisms.Contracts;

public enum ExecutionSource: ushort
{
    HttpApi,
    BackgroundWorker,
    MessageBroker,
    ServiceCall,
    Scheduler,
    Unknown
}

public interface IRequestContext
{

    void Initialize(ExecutionSource sourceType,
                    string? userId = "",
                    string? sourceId = "",
                    string? correlationId = null);
    string CorrelationId { get; }

    ExecutionSource SourceType { get; }

    string? UserId { get; }

    string? SourceId { get; }
}
