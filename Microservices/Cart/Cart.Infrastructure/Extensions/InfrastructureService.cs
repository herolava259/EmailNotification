using Cart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cart.Infrastructure.Extensions
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection serviceCollection,
                                                        IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ProductDBContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseConnectionString")));

            serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<ICartRepository, CartRepository>();
            serviceCollection.AddScoped<IListItemRepository, ListItemRepository>();

            return serviceCollection;
        }
    }
}
