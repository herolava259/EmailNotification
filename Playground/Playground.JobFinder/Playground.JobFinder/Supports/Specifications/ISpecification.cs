using System.Linq.Expressions;

namespace Playground.JobFinder.Supports.Specifications;

public interface IRootSpecification
{
}

public interface ISpecification<T> : IRootSpecification
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }

    bool IsSatisfiedBy(T obj);
}

public interface IGridSpecification<T> : IRootSpecification
{
    List<Expression<Func<T, bool>>> Criterias { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; set; }
}