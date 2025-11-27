using System.Linq.Expressions;

namespace Playground.JobFinder.Supports.Specifications;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public List<Expression<Func<T, object>>> ThenByClauses { get; private set; } = new();

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public List<Expression<Func<T, object>>> ThenByDescendingClauses { get; private set; } = new();
    public Expression<Func<T, object>>? GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
    {
        foreach (var include in includes)
        {
            AddInclude(include);
        }
    }

    protected void ApplyIncludeList(IEnumerable<string> includes)
    {
        foreach (var include in includes)
        {
            AddInclude(include);
        }
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }


    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }


    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
        OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
        OrderByDescending = orderByDescendingExpression;

    protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression) =>
        GroupBy = groupByExpression;


    protected virtual SpecificationBase<T> WithSortingClause(string sortBy, bool ascending, bool thenby = false)
    {
        //TODO: implement ApplySorting
        if(!thenby || OrderBy is null)
        {
            var paramExpr = Expression.Parameter(typeof(T),name: "x");
            var bodyExpr = Expression.Property(paramExpr, sortBy);
            var sortByClause = Expression.Lambda<Func<T, object>>(bodyExpr, paramExpr);
            if (ascending)
            {
                OrderBy = sortByClause;
            }
            else
                OrderByDescending = sortByClause;
            return this;

        }

        // chain expression clause order
        var tparamExpr = Expression.Parameter(typeof(T), name: "x");

        var tBodyExpr = Expression.Property(tparamExpr, sortBy);

        var tSortByClause = Expression.Lambda<Func<T, object>>(tBodyExpr, tparamExpr);

        if (ascending)
            ThenByClauses.Add(tSortByClause);

        else
            ThenByDescendingClauses.Add(tSortByClause);

        return this;
    }


    protected virtual void ApplySorting(string sort)
    {
        this.ApplySorting(sort, nameof(ApplyOrderBy), nameof(ApplyOrderByDescending));
    }

    private Func<T, bool>? _compiledExpression;

    private Func<T, bool> CompiledExpression
        => _compiledExpression ??= Criteria.Compile();

    public bool IsSatisfiedBy(T obj)
        => CompiledExpression(obj);
}
