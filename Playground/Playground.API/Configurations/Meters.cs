using System.Diagnostics.Metrics;

namespace Playground.API.Configurations;

public static class Meters
{
    public static readonly Meter GreeterMeter = new Meter("Otel.Example", "1.0.0");



    public static class Counters
    {
        public static readonly Counter<int> CountGreetings = GreeterMeter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");
    }
}
