using MassTransit.Util.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch;

public interface IWebSearchResult
{ }

public interface IWebSearchQuery
{
    //public string ToJsonString();
}

public interface ISearchClient<in TQuery, TResult>
    where TResult : IWebSearchResult
    where TQuery : IWebSearchQuery
{
    Task<TResult> SearchAsync(TQuery query, ulong timeout = 100);
    
}
