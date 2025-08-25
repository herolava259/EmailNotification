using Common.BaseDomain.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;



public interface IDomainCheckPoint<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    public string Name { get; }
    IMemento<TAggregateRoot> GetSnapshot();

    public void Catch(TAggregateRoot boundedContext);
}


public interface ITransactionalBoundary
{
    void Commit();
    void Rollback();

    void Rollback(string checkpointName);
}

public interface IAsyncTransactionalBoundary
{
    Task CommitAsync();
    Task RollbackAsync();

    Task RollbackAsync(string checkpointName);
}
public interface ITransactionalBoundary<TAggregateRoot>: IDisposable, ITransactionalBoundary
    where TAggregateRoot : AggregateRoot
{
    TAggregateRoot GetCurrentBoundedContext();
    

    IDomainCheckPoint<TAggregateRoot> CreateCheckPoint(string checkPointName);

    public void Rollback(IDomainCheckPoint<TAggregateRoot> checkPoint);

}


public interface IAsyncTransactionalBoundary<TAggregateRoot> : IAsyncDisposable, IAsyncTransactionalBoundary
    where TAggregateRoot : AggregateRoot
{
    TAggregateRoot GetCurrentBoundedContext();


    IDomainCheckPoint<TAggregateRoot> CreateCheckPoint(string checkPointName);

    public void RollbackAsync(IDomainCheckPoint<TAggregateRoot> checkPoint);

}