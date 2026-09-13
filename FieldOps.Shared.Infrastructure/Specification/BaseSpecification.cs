using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Abstractions.Specification;
using System.Linq.Expressions;

namespace FieldOps.Shared.Infrastructure.Specification;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = Criteria == null ? criteria : CombineCriteria(Criteria, criteria);
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPaging(PaginationParams pagination)
    {
        Skip = (pagination.PageNumber - 1) * pagination.PageSize;
        Take = pagination.PageSize;
        IsPagingEnabled = true;
    }

    private static Expression<Func<T, bool>> CombineCriteria(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));
        var combinedBody = Expression.AndAlso(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter)
        );
        return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
    }
}
