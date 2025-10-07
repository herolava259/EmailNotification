using System.Diagnostics.Metrics;

namespace Playground.WebApp.Config;

public static class DiagnosticConfig
{
    public const string ServiceName = "Playground";

    public static Meter Meter = new(ServiceName);

    public static Counter<int> SaleCounter = Meter.CreateCounter<int>("sale.count");
}
