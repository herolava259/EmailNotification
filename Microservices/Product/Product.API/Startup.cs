using Product.API.Services;
using Product.Application.Extensions;
using Product.Core.Repositories;
using Product.Infrastructure.Repositories;

namespace Product.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationService();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddAutoMapper(typeof(Startup));
        services.AddGrpc();


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<ProductService>();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(
                    "Communication with gRPC enpoints must be made through a gRPC client.");
            });
        });
    }
}
