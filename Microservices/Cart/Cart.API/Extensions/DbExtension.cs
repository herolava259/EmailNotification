using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();

            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation($"Start Db Migration: {typeof(TContext).Name}");
                CallSeeder(seeder, context, services);
                logger.LogInformation($"Migration Completed: {typeof(TContext).Name}");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, $"An error occured while migrating db {typeof(TContext).Name}");
            }
        }

        return host;
    }

    private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}