using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.DependencyInjections.Configurations;

public sealed record AiModelConfiguration(string ModelId, string EndpointName, string ApiKey)
{
}
