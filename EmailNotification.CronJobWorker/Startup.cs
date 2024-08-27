using BaseService.ApiService;
using BaseService.APIService;
using Coravel;
using Coravel.Scheduling.Schedule.Interfaces;
using EmailNotification.CronJobWorker.RemindChangePassword;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailNotification.CronJobWorker;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScheduler();
        services.AddTransient<RemindChangePasswordInvocable>();

        services.AddHttpClient<IRemindChangePasswordService, RemindChangePasswordService>();
        services.AddScoped<IRemindChangePasswordService, RemindChangePasswordService>();
    }

    public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
    {
        appBuilder.ApplicationServices.UseScheduler(scheduler =>
        {
            scheduler.OnWorker(nameof(RemindChangePasswordInvocable));
            scheduler.Schedule<RemindChangePasswordInvocable>()
                .EverySeconds(30)
                .PreventOverlapping(nameof(RemindChangePasswordInvocable));
        }).LogScheduledTaskProgress(appBuilder.ApplicationServices.GetService<ILogger<IScheduler>>());
    }
}