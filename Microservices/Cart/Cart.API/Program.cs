using Cart.API.Extensions;
using Cart.Infrastructure.Data;

namespace Cart.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .MigrateDatabase<CartDBContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<CartDBContext>>();
                CartDBContextSeed.SeedAsync(context, logger!).Wait();
            }).Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    }
}
