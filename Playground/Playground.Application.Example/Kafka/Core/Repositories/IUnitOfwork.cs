using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.Repositories;

public interface ICheckpoint
{

    string Name { get; }

    string Version { get; }

}

public interface IBoundedContextTransaction:IDisposable, IAsyncDisposable
{
    Task<ICheckpoint> AddCheckpointAsync(IAggregateRoot aggregateRoot);

    Task<bool> CommitAsync();

    Task<bool> RollbackAsync();

    Task<bool> RollbackTo(ICheckpoint checkpoint);
}

// TODO:  separate into  2 interfaces:  data access interface and data manipulation interface
public interface IUnitOfWork: IDisposable
{

    Task<bool> CompleteAsync();

    Task<IBoundedContextTransaction> BeginTransactionAsync(string name);
}
