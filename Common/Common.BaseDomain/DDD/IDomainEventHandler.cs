
using Common.Domain.Generic.Utility.ResultPattern;

namespace Common.Domain.Generic.DDD;



public interface IDomainEventHandler< in TDomainEvent,TResponse>
    where TDomainEvent: BaseDomainEvent
{
    Task<Result<TResponse>> HandleAsync(TDomainEvent domainEvent);

}
