namespace Playground.API.DI.Extensions;

public static class DistributedLockingDemoExtensions
{

    // demo with prue postgreql db using locking mechanism 
    public static void ConfigNpgsqlResource(this IHostApplicationBuilder builder)
        => builder.AddKeyedNpgsqlDataSource("distributed-locking");
}
