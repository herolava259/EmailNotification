using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Playground.JobFinder.Data;

namespace Playground.JobFinder.DependencyInjections;

public static class ApplicationDbConfigurationExtensions
{
    public static IServiceCollection AddDataAccessService(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=app.db"));
        services.AddScoped<IDbInitializer, IDbInitializer>();

        return services;
    }
    public static void MigrateApplicationDatabase(this IHost host)
    {
        var initializer = host.Services.GetRequiredService<IDbInitializer>();

        initializer.InitializeDataAsync().Wait();
    }
}
