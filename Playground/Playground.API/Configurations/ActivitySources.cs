using System.Diagnostics;

namespace Playground.API.Configurations;

public static class ActivitySources
{
    public static ActivitySource GreeterActivitySource = new ActivitySource("Otel.Greeter");
}
