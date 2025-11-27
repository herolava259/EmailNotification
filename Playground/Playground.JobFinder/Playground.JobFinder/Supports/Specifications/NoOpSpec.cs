using System.Linq.Expressions;

namespace Playground.JobFinder.Supports.Specifications;

public class NoOpSpec<TEntity> : SpecificationBase<TEntity>
{
    public override Expression<Func<TEntity, bool>> Criteria => p => true;
}
