using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.Abstractions;

public interface IConnector<TProvider>
    where TProvider : IStorageProvider
{
    Task<TProvider> CreateApi<TConfiguration>(TConfiguration config);
}
