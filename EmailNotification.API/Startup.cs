using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using EmailNotification.Application.Extensions;
using EmailNotification.Infrastructure.Extentions;
using EmailNotification.Infrastructure.Data;
using EmailNotification.EmailService;

namespace EmailNotification.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        }).AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        services.AddApplicationService();
        services.AddInfrastructureService(configuration: Configuration);
        services.AddAutoMapper(typeof(Startup));
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "EmailNotification.API",
                Version = "v1"
            });
        });

        services.AddHealthChecks().Services.AddDbContext<EmailNotificationDBContext>();

        services.AddScoped<IEmailService, EmailService.EmailService>();
        services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
    }

    public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            appBuilder.UseDeveloperExceptionPage();
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailNotification.API v1"));
        }

        appBuilder.UseRouting();
        appBuilder.UseCors("CorsPolicy");
        appBuilder.UseAuthorization();
        appBuilder.UseAuthentication();
        appBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });
    }
}
