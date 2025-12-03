using Medallion.Threading;
using Medallion.Threading.Postgres;

namespace Playground.API.DI.Extensions;

public static class DistributedLockingDemoExtensions
{

    // demo with prue postgreql db using locking mechanism 
    public static void ConfigNpgsqlResource(this IHostApplicationBuilder builder)
        => builder.AddKeyedNpgsqlDataSource("distributed-locking");

    // demo with DistributedLock package

    public static IServiceCollection AddPostgresDistributedLockProvider(this IHostApplicationBuilder builder)
        => builder.Services.AddSingleton<IDistributedLockProvider>(_ =>
        {
            return new PostgresDistributedSynchronizationProvider(
                builder.Configuration.GetConnectionString("distributed-locking")!);
        });
}
