using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Minio;

public static class MinioConfigExtension
{
    public static WebApplicationBuilder AddMinioConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MinioSettings>(builder.Configuration.GetSection("Minio"));
        return builder;
    }
    public static IServiceCollection AddMinioClient(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IMinioClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;
            return new MinioClient()
                        .WithEndpoint(settings.Endpoint)
                        .WithCredentials(settings.AccessKey, settings.SecretKey)
                        .WithSSL(settings.UseSSL)
                        .Build();

        });
        return services;
    }
}
