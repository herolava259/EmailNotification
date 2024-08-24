using EmailNotification.API;
using EmailNotification.API.Extensions;
using EmailNotification.Infrastructure.Data;

namespace EmailNotification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<EmailNotificationDBContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<EmailNotificationDBContext>>();
                    EmailNotificationDbContextSeed.SeedAsync(context, logger!).Wait();
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
}
