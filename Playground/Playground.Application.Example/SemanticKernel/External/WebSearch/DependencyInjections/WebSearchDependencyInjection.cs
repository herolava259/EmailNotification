using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Example.SemanticKernel.External.WebSearch.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.DependencyInjections;

public static class WebSearchDependencyInjection
{
    public static IServiceCollection AddSettingOptions(IServiceCollection services, IConfiguration configuration)
        => services.Configure<TavilySettings>(configuration.GetSection("Tavily")); // tavily config
    public static IServiceCollection AddHttpClientForTavilySearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new TavilySettings();

        configuration.GetSection("Tavily").Bind(settings);

        services.AddHttpClient("tavily-client", httpClient =>
        {
            httpClient.BaseAddress = new Uri(settings.DomainUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", settings.ApiKey);
        });

        return services;
    }

}
