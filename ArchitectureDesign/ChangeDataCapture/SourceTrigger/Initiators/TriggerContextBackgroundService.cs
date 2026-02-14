using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger.Initiators;

public abstract class TriggerContextBackgroundService(ITriggerContext _triggerContext,
                                                      IConfiguration _configuration) : BackgroundService
{
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _triggerContext.Initialize(TriggerSourceType.Internal,
                                   TriggerType.Background,
                                   _configuration["ServiceId"] ?? string.Empty,
                                   Guid.NewGuid().ToString());
        return base.StartAsync(cancellationToken);
    }
    
}
