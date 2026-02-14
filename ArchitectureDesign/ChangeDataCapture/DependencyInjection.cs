using ChangeDataCapture.Mechanisms.Contracts;
using ChangeDataCapture.Mechanisms.Implementations;
using ChangeDataCapture.SourceTrigger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture;

public static class DependencyInjection
{
    public static IServiceCollection RegisterNecessaryStuff(this IServiceCollection services)
    {
        services.AddSingleton<IRequestContext, RequestContext>();

        services.AddSingleton<IAmbientContext, TriggerContext>();

        services.AddSingleton<ITriggerContext, TriggerContext>();

        return services;
    }
}

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestContextMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestContextMiddleware>();
}
