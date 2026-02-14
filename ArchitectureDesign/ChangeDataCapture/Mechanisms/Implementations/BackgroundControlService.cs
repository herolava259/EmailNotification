using ChangeDataCapture.Mechanisms.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Mechanisms.Implementations;


public abstract class ServiceSettings
{
    public string ServiceName { get; set; }
}

public abstract class BackgroundControlService: BackgroundService
{
    private readonly IRequestContext _requestContext;
    private readonly string _serviceName;


    protected BackgroundControlService(IRequestContext requestContext, IOptions<ServiceSettings> settings)
    {
        _requestContext = requestContext;
        _serviceName = settings.Value.ServiceName;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _requestContext.Initialize(sourceType: ExecutionSource.BackgroundWorker,
                                   correlationId: Guid.NewGuid().ToString(),
                                   sourceId: _serviceName);
        return base.StartAsync(cancellationToken);
    }
}
