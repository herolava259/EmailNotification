using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Playground.API.DI.Extensions;

public static class OtelExtensions
{
    public static WebApplicationBuilder AddOpenTelemery(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Playground.API"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                ;
                metrics.AddOtlpExporter(options => options.Endpoint = new Uri("http://bookstore.monitoring.dashboard:18888"));
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();
                tracing.AddOtlpExporter(options => options.Endpoint = new Uri("http://bookstore.monitoring.dashboard:18888"));
            });

            builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

        return builder;
    }
}
