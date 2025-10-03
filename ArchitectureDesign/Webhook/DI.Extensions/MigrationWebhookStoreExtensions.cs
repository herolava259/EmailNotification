using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Webhook.Constants;

namespace Webhook.DI.Extensions;

public static class MigrationWebhookStoreExtensions
{
    public static IHost MigrateWebhookScheme<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var config = services.GetRequiredService<IConfiguration>();


            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Product DB Migration Started.");
                ApplyMigrations(config);
                logger.LogInformation("Product DB Migration Completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

            }
        }

        return host;
    }
    private static void ApplyMigrations(IConfiguration configuration)
    {
        using var connection = new NpgsqlConnection(configuration["Webhook:Store:Posgresql:ConnectionStrings"]);

        connection.Open();

        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };


        cmd.CommandText = PostgresqlScript.SubscriptionTableCreationScript;

        cmd.ExecuteNonQuery();


    }

}
