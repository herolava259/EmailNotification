

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Product.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var config = services.GetRequiredService<IConfiguration>();


            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Product DB Migration Started.");
                ApllyMigrations(config);
                logger.LogInformation("Product DB Migration Completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

            }
        }

        return host;
    }

    private static void ApllyMigrations(IConfiguration config)
    {
        var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));

        connection.Open();

        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };

        cmd.CommandText = "DROP TABLE IF EXISTS Product";

        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE Product(Id uuid DEFAULT gen_random_uuid(), 
                                                    ProductName VARCHAR(500) NOT NULL,
                                                    Price numeric,
                                                    Quantity INT,
                                                    PRIMARY KEY (Id)
                                                )";

        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductName, Price, Quantity) VALUES('Adidas Quick Force Indoor Badminton Shoes', 1200, 500);";


        cmd.ExecuteNonQuery();

    }
}
