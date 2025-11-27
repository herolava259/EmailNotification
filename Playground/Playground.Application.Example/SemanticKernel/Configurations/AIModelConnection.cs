using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Configurations;

public sealed class AIModelConnection
{
    public string ModelId { get; init; } = string.Empty;

    public string Endpoint { get; init; } = String.Empty;

    public string ApiKey { get; init; } = String.Empty;
}
