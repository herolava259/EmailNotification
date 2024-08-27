using Cart.Application.Behaviours;
using Cart.Application.Services.Behaviours;
using Cart.Application.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Product.Grpc.Protos;
using Microsoft.Extensions.Configuration;

namespace Cart.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:ProductUrl"]!));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}