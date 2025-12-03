using Medallion.Threading;
using Medallion.Threading.Postgres;
using Medallion.Threading.Redis;
using StackExchange.Redis;

namespace Playground.API.DI.Extensions;

public static class DistributedLockingDemoExtensions
{

    // demo with prue postgreql db using locking mechanism 
    public static void ConfigureNpgsqlResource(this IHostApplicationBuilder builder)
        => builder.AddKeyedNpgsqlDataSource("distributed-locking");

    // config for demo: using redis like cache
    public static void ConfigureRedisProvider(this IHostApplicationBuilder builder)
        => builder.AddRedisClient("redis");

    // demo with DistributedLock package

    public static IServiceCollection AddPostgresDistributedLockProvider(this IHostApplicationBuilder builder)
        => builder.Services.AddKeyedSingleton<IDistributedLockProvider>("postgres", (sp, _) =>
        {
            return new PostgresDistributedSynchronizationProvider(
                builder.Configuration.GetConnectionString("distributed-locking")!);
        });


    // explain redis connection multiplexer: https://redis.io/blog/multiplexing-explained/
    // what is multiplexing connection?
    // is a kaind of connection management (like the concept connection pool) but instead of
    // creating multiple connection and keep them into a pool, will retrieve a connection from the pool to using 
    // With multilplexing, one take multiple threads and share a single connection. 
    // keep in mind: multiplexing like a rope being braided. Many strands are arranged in a particular way to yield a single at 
    // the other end. 
    /// multiplexing has some same point with pipelining but in the redis sense, meaning sending commands to the server without regard for the
    /// responsd being received => faster, it removes the latency between and awaiting the response to each item
    /// cons of multiplexing: because merging requests from multiple source into only one connections, low/haevy threads affect to other threads
    public static IServiceCollection AddRedisDistributedLockProvider(this IHostApplicationBuilder builder)
        => builder.Services.AddKeyedSingleton<IDistributedLockProvider>("redis", (sp, _) =>
        {
            var connMultiplexer = sp.GetRequiredService<ConnectionMultiplexer>();

            return new RedisDistributedSynchronizationProvider(connMultiplexer.GetDatabase());
        });
}
