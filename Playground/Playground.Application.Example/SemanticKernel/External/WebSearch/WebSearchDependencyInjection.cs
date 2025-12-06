using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch;

public static class WebSearchDependencyInjection
{
    public static IServiceCollection AddHttpClientForTavilySearch(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient("tavily-client", httpClient =>
        {
            http
        });
    }
}
