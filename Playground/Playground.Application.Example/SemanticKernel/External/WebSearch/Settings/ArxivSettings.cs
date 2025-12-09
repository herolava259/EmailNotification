using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.Settings;

public sealed class ArxivSettings
{
    public string QueryUrlFormat { get; set; } = "https://export.arxiv.org/api/query?{}";

    public uint PageSize { get; set; }

    public uint DelaySeconds { get; set; }

    public uint MaxRetry { get; set; }


}
