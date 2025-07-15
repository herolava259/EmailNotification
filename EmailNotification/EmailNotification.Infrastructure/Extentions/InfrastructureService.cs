using EmailNotification.Core.Repositories;
using EmailNotification.Infrastructure.Data;
using EmailNotification.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailNotification.Infrastructure.Extentions
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection serviceCollection,
                                                        IConfiguration configuration)
        {
            serviceCollection.AddDbContext<EmailNotificationDBContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseConnectionString")));

            serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserAccountRepository, UserAccountRepository>();
            serviceCollection.AddScoped<IProfileRepository, ProfileRepository>();

            return serviceCollection;
        }
    }
}
