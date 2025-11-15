using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.Repositories;

public interface IGenericRepository
{
}

public interface IGenericRepository<TEntity>
    where TEntity: EntityBase
{
    public Task<TEntity?> FindAsync(Guid id);

    public Task<(bool, TEntity?)> CreateAsync(Guid id);
}
