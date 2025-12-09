using Playground.API.Configurations;

namespace Playground.API.Endpoints;

public static class DemoUsingOpenTelemetryEndpoints
{
    public const string PathPrefix = "otel-demo";
    private static readonly string SendGreeterRoute = $"{PathPrefix}/send-greeter";

    public static RouteHandlerBuilder MapSendGreeter(this IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapGet(SendGreeterRoute,
                                (ILogger<Program> logger) =>
                                {
                                    using var activity = ActivitySources.GreeterActivitySource.StartActivity("GreeterActivity");
                                    logger.LogInformation("Sending Greeting");

                                    Meters.Counters.CountGreetings.Add(1);

                                    activity?.SetTag("greeting", "Hello World");

                                    return Results.Ok("Hello World");
                                });
}
