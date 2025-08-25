using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.EventSourcing.ExternalInteraction.Gateway;

public interface IExternalGateway
{
}

public interface ICacheExternalRequestResult
{
    public Task<TResult> GetAsync<TResult, TQuery>(TQuery query);

    public Task<string> CacheResultAsync<TResult, TQuery>(TQuery query, TResult result);
}

public interface IExternalService { }
public interface IExternalGateway<out TExternalService>: IExternalService 
    where TExternalService : IExternalService
{
    public TExternalService Service { get; }


}
