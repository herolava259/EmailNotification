using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.Settings;

public sealed class TavilySettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string DomainUrl { get; set; } = "https://api.tavily.com";
}
