using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Webhook.Constants;

namespace Webhook.Implementation;

internal class WebhookBroadcastWorker(
    IServiceScopeFactory _scopeFactory,
    ChannelReader<WebhookEvent> _reader) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var ev in _reader.ReadAllAsync(stoppingToken))
        {
            using var activity = DiagnosticConfiguration.Source.StartActivity(
                $"{ev.ParentActivityId} is dispatching webhook event",
                ActivityKind.Internal,
                parentId: ev.ParentActivityId);
            using var scope = _scopeFactory.CreateScope();

            var dispatcher = scope.ServiceProvider.GetRequiredService<WebhookProcessor>();

            await dispatcher.DispatchAsync(ev.EventType, ev.Payload);

            activity?.Stop();
        }


    }
}
