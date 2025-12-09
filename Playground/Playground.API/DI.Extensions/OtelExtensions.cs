using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Playground.API.DI.Extensions;

public static class OtelExtensions
{
    public static WebApplicationBuilder AddOpenTelemery(this WebApplicationBuilder builder)
    {
        var metricEndpoint = builder.Configuration["OTEL_EXPORTER_METRIC_ENDPOINT"];
        var tracingEndpoint = builder.Configuration["OTEL_EXPORTER_TRACING_ENDPOINT"];

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Playground.API"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                ;
                metrics.AddOtlpExporter(options => options.Endpoint = new Uri(metricEndpoint!));
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();
                tracing.AddOtlpExporter(options => options.Endpoint = new Uri(tracingEndpoint!));
            });

        builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

        return builder;
    }

    public static WebApplicationBuilder AddOtelCustom(this WebApplicationBuilder builder, IEnumerable<Meter> meters,IEnumerable<ActivitySource> activitySources)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        var otel = builder.Services.AddOpenTelemetry();

        otel.WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddMeter();
            foreach (var meter in meters)
            {
                metrics.AddMeter(meter.Name);
            }

            metrics.AddMeter("Microsoft.AspNetCore.Hosting");
            metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        });

        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            foreach (var source in activitySources)
            {
                tracing.AddSource(source.Name);
            }
        });

        // export data external otel collector
        var otlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

        if (otlpEndpoint != null)
            otel.UseOtlpExporter();

        return builder;
    }
}
