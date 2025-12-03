using Dapper;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace Playground.API.Endpoints;

public static class DemoDistributedLockingEndpoints
{
    // reference: https://www.youtube.com/watch?v=SzLhUq1Hezk
    // how to posgresql provide distributed locking : https://medium.com/thefreshwrites/advisory-locks-in-postgres-1f993647d061
    public const string PostgresqlRawLockingRoute = "";
    public static RouteHandlerBuilder MapDistributedLockingOnlyRawLockingOfPostgreql(this IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapPost(PostgresqlRawLockingRoute, async (NpgsqlDataSource dataSource) =>
        {
            await using var connection = dataSource.CreateConnection();

            static long HashKey(string key) => BitConverter.ToInt64(SHA256.HashData(Encoding.UTF8.GetBytes(key)), 0);

            static Task DoWork() => Task.Delay(5000);

            var key = HashKey("borrow-book");

            var acquired = await connection.ExecuteScalarAsync<bool>("SELECT pg_try_advisory_lock(@key)", new
            {
                key
            });

            if (!acquired)
                return Results.Conflict("Require key is failure");

            try
            {
                await DoWork();
            }
            finally
            {
                await connection.ExecuteAsync(
                    "SELECT pg_advisory_unlock(@key)",
                    new {key});
            }
        }); 

}
